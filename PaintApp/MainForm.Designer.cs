using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PaintApp.Controls;
using PaintApp.Helpers;
using PaintApp.Models;
using PaintApp.Plugins;

namespace PaintApp
{
    public partial class MainForm : Form
    {
        private Color lineColor = Color.Black;
        private Color fillColor = Color.White;

        public MainForm()
        {
            InitializeComponent();

            for (int i = 1; i <= 20; i++) cmbThickness.Items.Add(i);
            cmbThickness.SelectedItem = 2;

            UpdateLayerList();
            dockPanel.Width = 220;
        }

        private void UpdateLayerList()
        {
            lstLayers.Items.Clear();
            cmbCurrentLayer.Items.Clear();
            foreach (var layer in canvas.LayerManager.Layers)
            {
                lstLayers.Items.Add(layer.Name);
                cmbCurrentLayer.Items.Add(layer.Name);
            }
            if (canvas.LayerManager.CurrentLayer != null)
            {
                int idx = canvas.LayerManager.Layers.FindIndex(l => l.Id == canvas.LayerManager.CurrentLayer.Id);
                if (idx >= 0)
                {
                    lstLayers.SelectedIndex = idx;
                    cmbCurrentLayer.SelectedIndex = idx;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();
        private void undoToolStripMenuItem_Click(object sender, EventArgs e) => canvas.Undo();
        private void redoToolStripMenuItem_Click(object sender, EventArgs e) => canvas.Redo();
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) => canvas.CopySelectedShapes();
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => canvas.PasteShapes();
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => canvas.DeleteSelectedShapes();

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все фигуры?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                canvas.DeleteAllShapes();
        }

        private void rotate90ToolStripMenuItem_Click(object sender, EventArgs e) => canvas.RotateSelectedShapes(90);

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Paint - редактор фигур\nВерсия 2.0", "О программе");
        }

        private void btnSegment_Click(object sender, EventArgs e) => canvas.SetDrawingMode(DrawingMode.Segment);
        private void btnPolyline_Click(object sender, EventArgs e) => canvas.SetDrawingMode(DrawingMode.Polyline);
        private void btnEllipse_Click(object sender, EventArgs e) => canvas.SetDrawingMode(DrawingMode.Ellipse);
        private void btnRectangle_Click(object sender, EventArgs e) => canvas.SetDrawingMode(DrawingMode.Rectangle);
        private void btnPolygon_Click(object sender, EventArgs e) => canvas.SetDrawingMode(DrawingMode.Polygon);
        private void btnSelect_Click(object sender, EventArgs e) => canvas.SetDrawingMode(DrawingMode.Select);

        private void cmbThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbThickness.SelectedItem == null) return;
            int thickness = (int)cmbThickness.SelectedItem;
            using (Pen p = new Pen(lineColor, thickness))
            {
                p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                canvas.ChangeSelectedPen(p);
                canvas.CurrentPen = p;
            }
        }

        private void btnLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = lineColor;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                lineColor = dlg.Color;
                using (Pen p = new Pen(lineColor, (int)cmbThickness.SelectedItem))
                {
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    canvas.ChangeSelectedPen(p);
                    canvas.CurrentPen = p;
                }
            }
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = fillColor;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fillColor = dlg.Color;
                canvas.ChangeSelectedBrush(new SolidBrush(fillColor));
                canvas.CurrentBrush = new SolidBrush(fillColor);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e) => canvas.Undo();
        private void btnRedo_Click(object sender, EventArgs e) => canvas.Redo();
        private void btnCopy_Click(object sender, EventArgs e) => canvas.CopySelectedShapes();
        private void btnPaste_Click(object sender, EventArgs e) => canvas.PasteShapes();
        private void btnDelete_Click(object sender, EventArgs e) => canvas.DeleteSelectedShapes();

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить все фигуры?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                canvas.DeleteAllShapes();
        }

        private void btnRotateLeft_Click(object sender, EventArgs e) => canvas.RotateSelectedShapes(-15);
        private void btnRotateRight_Click(object sender, EventArgs e) => canvas.RotateSelectedShapes(15);

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Название слоя", "Новый слой", $"Слой {canvas.LayerManager.Layers.Count + 1}");
            if (!string.IsNullOrEmpty(name))
            {
                canvas.LayerManager.AddLayer(name);
                UpdateLayerList();
            }
        }

        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            if (lstLayers.SelectedIndex >= 0 && canvas.LayerManager.Layers.Count > 1)
            {
                int id = canvas.LayerManager.Layers[lstLayers.SelectedIndex].Id;
                canvas.LayerManager.RemoveLayer(id);
                UpdateLayerList();
                canvas.Invalidate();
            }
            else MessageBox.Show("Нельзя удалить последний слой или слой не выбран.");
        }

        private void btnLayerUp_Click(object sender, EventArgs e)
        {
            if (lstLayers.SelectedIndex > 0)
            {
                int id = canvas.LayerManager.Layers[lstLayers.SelectedIndex].Id;
                canvas.LayerManager.MoveLayerUp(id);
                UpdateLayerList();
                canvas.Invalidate();
            }
        }

        private void btnLayerDown_Click(object sender, EventArgs e)
        {
            if (lstLayers.SelectedIndex >= 0 && lstLayers.SelectedIndex < canvas.LayerManager.Layers.Count - 1)
            {
                int id = canvas.LayerManager.Layers[lstLayers.SelectedIndex].Id;
                canvas.LayerManager.MoveLayerDown(id);
                UpdateLayerList();
                canvas.Invalidate();
            }
        }

        private void btnMoveToLayer_Click(object sender, EventArgs e)
        {
            if (canvas.SelectedShapes.Count == 0)
            {
                MessageBox.Show("Нет выделенных фигур.");
                return;
            }
            if (cmbCurrentLayer.SelectedIndex >= 0)
            {
                int targetId = canvas.LayerManager.Layers[cmbCurrentLayer.SelectedIndex].Id;
                foreach (var shape in canvas.SelectedShapes)
                    canvas.LayerManager.MoveShapeToLayer(shape, targetId);
                canvas.Invalidate();
            }
        }

        private void lstLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLayers.SelectedIndex >= 0)
            {
                int id = canvas.LayerManager.Layers[lstLayers.SelectedIndex].Id;
                canvas.LayerManager.SetCurrentLayer(id);
                cmbCurrentLayer.SelectedIndex = lstLayers.SelectedIndex;
            }
        }

        private void cmbCurrentLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCurrentLayer.SelectedIndex >= 0)
            {
                int id = canvas.LayerManager.Layers[cmbCurrentLayer.SelectedIndex].Id;
                canvas.LayerManager.SetCurrentLayer(id);
                lstLayers.SelectedIndex = cmbCurrentLayer.SelectedIndex;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON files (*.json)|*.json";
            sfd.DefaultExt = "json";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SerializationHelper.SaveToFile(sfd.FileName, canvas.LayerManager.Layers);
                    MessageBox.Show("Сохранено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON files (*.json)|*.json";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newLayers = SerializationHelper.LoadFromFile(ofd.FileName);
                    canvas.LayerManager.ReplaceLayers(newLayers);
                    canvas.Invalidate();
                    UpdateLayerList();
                    MessageBox.Show("Загружено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "DLL files (*.dll)|*.dll";
            ofd.Title = "Выберите DLL с плагином";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(ofd.FileName);
                    var pluginTypes = assembly.GetTypes().Where(t => typeof(IShapePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in pluginTypes)
                    {
                        var plugin = (IShapePlugin)Activator.CreateInstance(type);
                        AddPluginToMenu(plugin);
                    }

                    if (pluginTypes.Count() > 0)
                        MessageBox.Show($"Загружено {pluginTypes.Count()} плагинов!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("В выбранной DLL не найдено плагинов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки плагина: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddPluginToMenu(IShapePlugin plugin)
        {
            ToolStripMenuItem newItem = new ToolStripMenuItem(plugin.ShapeTypeName);
            newItem.Click += (s, ev) => {
                canvas.SetPlugin(plugin);
                this.Text = $"Графический редактор - Режим: {plugin.ShapeTypeName}";
            };
            ddbShapes.DropDownItems.Add(newItem);
        }
    }
}