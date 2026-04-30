using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using PaintApp.Models;
using PaintApp.Commands;
using PaintApp.Plugins;

namespace PaintApp.Controls
{
    public enum DrawingMode { None, Segment, Polyline, Ellipse, Rectangle, Polygon, Select, Plugin }

    [DesignTimeVisible(false)]
    public class CanvasControl : UserControl
    {
        private DrawingMode currentMode = DrawingMode.None;
        private Pen currentPen = new Pen(Color.Black, 2);
        private Brush currentBrush = new SolidBrush(Color.White);
        private List<Shape> shapes = new List<Shape>();
        private Shape currentShape = null;
        private List<Shape> selectedShapes = new List<Shape>();
        private Point startPoint, lastPoint;
        private bool isDrawing = false;
        private bool isSelecting = false;
        private Point selectionStart;
        private Rectangle selectionRect;
        private List<Shape> clipboard = new List<Shape>();
        private CommandManager commandManager = new CommandManager();
        private LayerManager layerManager = new LayerManager();
        private bool isResizing = false;
        private Shape resizingShape = null;
        private Point resizeStartPoint;
        private int resizeHandle = -1;
        private IShapePlugin currentPlugin = null;

        public CanvasControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            MouseDown += CanvasControl_MouseDown;
            MouseMove += CanvasControl_MouseMove;
            MouseUp += CanvasControl_MouseUp;
            Paint += CanvasControl_Paint;
            KeyDown += CanvasControl_KeyDown;
            MouseWheel += CanvasControl_MouseWheel;
            SetStyle(ControlStyles.Selectable, true);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pen CurrentPen
        {
            get => currentPen;
            set
            {
                if (value == null)
                {
                    currentPen = new Pen(Color.Black, 2);
                }
                else
                {
                    try
                    {
                        float w = (value.Width > 0 && !float.IsNaN(value.Width)) ? value.Width : 2f;
                        var newPen = new Pen(value.Color, w);
                        newPen.StartCap = LineCap.Round;
                        newPen.EndCap = LineCap.Round;
                        newPen.LineJoin = LineJoin.Round;
                        currentPen = newPen;
                    }
                    catch
                    {
                        currentPen = new Pen(Color.Black, 2);
                        currentPen.StartCap = LineCap.Round;
                        currentPen.EndCap = LineCap.Round;
                        currentPen.LineJoin = LineJoin.Round;
                    }
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Brush CurrentBrush
        {
            get => currentBrush;
            set
            {
                if (value is SolidBrush sb)
                    currentBrush = new SolidBrush(sb.Color);
                else
                    currentBrush = new SolidBrush(Color.White);
            }
        }

        public void SetDrawingMode(DrawingMode mode) { currentMode = mode; currentPlugin = null; if (mode != DrawingMode.Select) ClearSelection(); Invalidate(); }

        public void SetPlugin(IShapePlugin plugin)
        {
            currentPlugin = plugin;
            currentMode = DrawingMode.Plugin;
        }

        public void ChangeSelectedPen(Pen pen) { if (selectedShapes.Count == 0) return; foreach (var s in selectedShapes) commandManager.ExecuteCommand(new ModifyShapeCommand(s, pen, s.Brush)); Invalidate(); }
        public void ChangeSelectedBrush(Brush brush) { if (selectedShapes.Count == 0) return; foreach (var s in selectedShapes) commandManager.ExecuteCommand(new ModifyShapeCommand(s, s.Pen, brush)); Invalidate(); }
        public void DeleteSelectedShapes() { if (selectedShapes.Count == 0) return; var toDel = new List<Shape>(selectedShapes); foreach (var l in layerManager.Layers) foreach (var s in toDel) l.RemoveShape(s); commandManager.ExecuteCommand(new RemoveShapeCommand(toDel, shapes)); selectedShapes.Clear(); Invalidate(); }
        public void DeleteAllShapes() { commandManager.ExecuteCommand(new RemoveAllShapesCommand(shapes)); foreach (var l in layerManager.Layers) l.ClearShapes(); selectedShapes.Clear(); Invalidate(); }
        public void CopySelectedShapes() { clipboard.Clear(); foreach (var s in selectedShapes) clipboard.Add(s.Clone()); }

        public void PasteShapes()
        {
            foreach (var s in clipboard)
            {
                var clone = s.Clone();
                clone.Move(10, 10);
                if (layerManager.CurrentLayer != null)
                    commandManager.ExecuteCommand(new AddShapeCommand(clone, layerManager.CurrentLayer, shapes));
            }
            Invalidate();
        }

        public void RotateSelectedShapes(double angle) { if (selectedShapes.Count == 0) return; commandManager.ExecuteCommand(new RotateShapeCommand(new List<Shape>(selectedShapes), angle)); Invalidate(); }
        public void MoveSelectedShapes(int dx, int dy) { if (selectedShapes.Count == 0) return; commandManager.ExecuteCommand(new MoveShapeCommand(selectedShapes, dx, dy)); Invalidate(); }

        public void Undo() { commandManager.Undo(); RefreshSelection(); Invalidate(); }
        public void Redo() { commandManager.Redo(); RefreshSelection(); Invalidate(); }

        public void RefreshView() => Invalidate();
        public List<Shape> SelectedShapes => selectedShapes;
        public LayerManager LayerManager => layerManager;

        private void RefreshSelection() { selectedShapes.Clear(); foreach (var l in layerManager.Layers) foreach (var s in l.Shapes) if (s.Selected) selectedShapes.Add(s); }
        private void ClearSelection() { foreach (var s in selectedShapes) s.Selected = false; selectedShapes.Clear(); }
        private Shape GetShapeAt(Point p) { for (int l = layerManager.Layers.Count - 1; l >= 0; l--) { if (!layerManager.Layers[l].Visible) continue; for (int i = layerManager.Layers[l].Shapes.Count - 1; i >= 0; i--) if (layerManager.Layers[l].Shapes[i].Contains(p)) return layerManager.Layers[l].Shapes[i]; } return null; }
        private int GetResizeHandle(Point pos, Rectangle rect)
        { if (new Rectangle(rect.Left - 5, rect.Top - 5, 10, 10).Contains(pos)) return 0; if (new Rectangle(rect.Right - 5, rect.Top - 5, 10, 10).Contains(pos)) return 1; if (new Rectangle(rect.Left - 5, rect.Bottom - 5, 10, 10).Contains(pos)) return 2; if (new Rectangle(rect.Right - 5, rect.Bottom - 5, 10, 10).Contains(pos)) return 3; return -1; }

        private void DrawRotationArrows(Graphics g, Rectangle rect)
        {
            int cx = rect.Center().X, top = rect.Top - 20;
            using (Pen p = new Pen(Color.DarkGreen, 2)) using (Brush b = new SolidBrush(Color.Green))
            {
                Point[] left = { new Point(cx - 20, top), new Point(cx - 20, top - 8), new Point(cx - 30, top - 8), new Point(cx - 30, top - 15), new Point(cx - 40, top), new Point(cx - 30, top + 15), new Point(cx - 30, top + 8), new Point(cx - 20, top + 8) };
                Point[] right = { new Point(cx + 20, top), new Point(cx + 20, top - 8), new Point(cx + 30, top - 8), new Point(cx + 30, top - 15), new Point(cx + 40, top), new Point(cx + 30, top + 15), new Point(cx + 30, top + 8), new Point(cx + 20, top + 8) };
                g.FillPolygon(b, left); g.DrawPolygon(p, left); g.FillPolygon(b, right); g.DrawPolygon(p, right);
            }
        }

        private bool OnLeftArrow(Point pos, Rectangle rect) { int cx = rect.Center().X, top = rect.Top - 20; return pos.X >= cx - 40 && pos.X <= cx - 20 && pos.Y >= top - 15 && pos.Y <= top + 15; }
        private bool OnRightArrow(Point pos, Rectangle rect) { int cx = rect.Center().X, top = rect.Top - 20; return pos.X >= cx + 20 && pos.X <= cx + 40 && pos.Y >= top - 15 && pos.Y <= top + 15; }

        private void CanvasControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);
            foreach (var layer in layerManager.Layers)
            {
                if (!layer.Visible) continue;
                foreach (var shape in layer.Shapes)
                {
                    if (shape.Selected)
                    {
                        var rect = shape.GetBoundingRect();
                        using (Pen selPen = new Pen(Color.Blue, 2)) using (Pen black = new Pen(Color.Black, 1)) using (Brush white = new SolidBrush(Color.White))
                        { e.Graphics.DrawRectangle(selPen, rect); e.Graphics.FillRectangle(white, rect.Left - 4, rect.Top - 4, 8, 8); e.Graphics.DrawRectangle(black, rect.Left - 4, rect.Top - 4, 8, 8); e.Graphics.FillRectangle(white, rect.Right - 4, rect.Top - 4, 8, 8); e.Graphics.DrawRectangle(black, rect.Right - 4, rect.Top - 4, 8, 8); e.Graphics.FillRectangle(white, rect.Left - 4, rect.Bottom - 4, 8, 8); e.Graphics.DrawRectangle(black, rect.Left - 4, rect.Bottom - 4, 8, 8); e.Graphics.FillRectangle(white, rect.Right - 4, rect.Bottom - 4, 8, 8); e.Graphics.DrawRectangle(black, rect.Right - 4, rect.Bottom - 4, 8, 8); }
                        DrawRotationArrows(e.Graphics, shape.GetBoundingRect());
                    }
                    shape.Draw(e.Graphics);
                }
            }
            if (isSelecting && selectionRect != Rectangle.Empty) { using (Pen dash = new Pen(Color.Blue, 1) { DashStyle = DashStyle.Dash }) using (Brush fill = new SolidBrush(Color.FromArgb(50, 0, 0, 255))) { e.Graphics.FillRectangle(fill, selectionRect); e.Graphics.DrawRectangle(dash, selectionRect); } }
            if (isDrawing && currentShape != null) currentShape.DrawPreview(e.Graphics, lastPoint);
        }

        private void CanvasControl_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                if (currentMode == DrawingMode.Select && selectedShapes.Count > 0)
                {
                    foreach (var s in selectedShapes.ToList())
                    {
                        var rect = s.GetBoundingRect();
                        if (OnLeftArrow(e.Location, rect)) { RotateSelectedShapes(-15); return; }
                        if (OnRightArrow(e.Location, rect)) { RotateSelectedShapes(15); return; }
                    }
                }
                if (currentMode == DrawingMode.Select && selectedShapes.Count > 0)
                {
                    foreach (var s in selectedShapes.ToList())
                    {
                        int handle = GetResizeHandle(e.Location, s.GetBoundingRect());
                        if (handle != -1) { isResizing = true; resizingShape = s; resizeStartPoint = e.Location; resizeHandle = handle; return; }
                    }
                }
                if (currentMode == DrawingMode.Select)
                {
                    Shape clicked = GetShapeAt(e.Location);
                    if (clicked != null)
                    {
                        if (ModifierKeys.HasFlag(Keys.Control)) { clicked.Selected = !clicked.Selected; if (clicked.Selected) selectedShapes.Add(clicked); else selectedShapes.Remove(clicked); }
                        else if (!clicked.Selected) { ClearSelection(); clicked.Selected = true; selectedShapes.Add(clicked); }
                        lastPoint = e.Location; Invalidate();
                    }
                    else if (!ModifierKeys.HasFlag(Keys.Control)) { isSelecting = true; selectionStart = e.Location; selectionRect = new Rectangle(selectionStart, new Size(0, 0)); }
                }
                else if (currentMode == DrawingMode.Polygon)
                {
                    isDrawing = true; startPoint = e.Location; currentShape = new PolygonShape();
                    currentShape.ApplyPen(currentPen); currentShape.ApplyBrush(currentBrush); currentShape.SetStartPoint(startPoint);
                }
                else if (currentMode == DrawingMode.Polyline)
                {
                    if (!isDrawing)
                    {
                        isDrawing = true; startPoint = e.Location; currentShape = new Polyline();
                        currentShape.ApplyPen(currentPen); currentShape.ApplyBrush(currentBrush); currentShape.SetStartPoint(startPoint);
                    }
                    else
                    {
                        if (currentShape is Polyline poly) { poly.AddPoint(e.Location); Invalidate(); }
                    }
                }
                else if (currentMode == DrawingMode.Plugin && currentPlugin != null)
                {
                    isDrawing = true;
                    startPoint = e.Location;
                    currentShape = currentPlugin.CreateInstance();
                    currentShape.ApplyPen(currentPen);
                    currentShape.ApplyBrush(currentBrush);
                    currentShape.SetStartPoint(startPoint);
                }
                else
                {
                    isDrawing = true; startPoint = e.Location;
                    switch (currentMode)
                    {
                        case DrawingMode.Segment: currentShape = new Segment(); break;
                        case DrawingMode.Ellipse: currentShape = new EllipseShape(); break;
                        case DrawingMode.Rectangle: currentShape = new RectangleShape(); break;
                    }
                    if (currentShape != null) { currentShape.ApplyPen(currentPen); currentShape.ApplyBrush(currentBrush); currentShape.SetStartPoint(startPoint); }
                }
            }
            else if (e.Button == MouseButtons.Right && isDrawing && currentMode == DrawingMode.Polyline)
            {
                if (currentShape is Polyline poly)
                {
                    poly.FinishDrawing();
                    if (poly.Points.Count >= 2)
                    {
                        if (layerManager.CurrentLayer != null)
                            commandManager.ExecuteCommand(new AddShapeCommand(currentShape, layerManager.CurrentLayer, shapes));
                    }
                    currentShape = null;
                    isDrawing = false;
                    Invalidate();
                }
            }
        }

        private void CanvasControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isResizing && resizingShape != null)
                { Point delta = new Point(e.X - resizeStartPoint.X, e.Y - resizeStartPoint.Y); Rectangle orig = resizingShape.GetBoundingRect(); commandManager.ExecuteCommand(new ResizeShapeCommand(resizingShape, delta, resizeHandle, orig)); resizeStartPoint = e.Location; Invalidate(); }
                else if (isDrawing && currentShape != null) { lastPoint = e.Location; Invalidate(); }
                else if (isSelecting) { selectionRect = new Rectangle(Math.Min(selectionStart.X, e.X), Math.Min(selectionStart.Y, e.Y), Math.Abs(selectionStart.X - e.X), Math.Abs(selectionStart.Y - e.Y)); Invalidate(); }
                else if (currentMode == DrawingMode.Select && selectedShapes.Count > 0 && (e.Button & MouseButtons.Left) != 0) { int dx = e.X - lastPoint.X, dy = e.Y - lastPoint.Y; if (dx != 0 || dy != 0) MoveSelectedShapes(dx, dy); lastPoint = e.Location; }
                else lastPoint = e.Location;
            }
            catch { }
        }

        private void CanvasControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isResizing) { isResizing = false; resizingShape = null; }
                else if (isDrawing && currentShape != null && currentMode == DrawingMode.Polygon)
                {
                    currentShape.SetEndPoint(e.Location);
                    if (layerManager.CurrentLayer != null)
                        commandManager.ExecuteCommand(new AddShapeCommand(currentShape, layerManager.CurrentLayer, shapes));
                    currentShape = null;
                    isDrawing = false;
                    Invalidate();
                }
                else if (isDrawing && currentShape != null && (currentMode != DrawingMode.Polyline && currentMode != DrawingMode.Polygon))
                {
                    currentShape.SetEndPoint(e.Location);
                    if (layerManager.CurrentLayer != null)
                        commandManager.ExecuteCommand(new AddShapeCommand(currentShape, layerManager.CurrentLayer, shapes));
                    currentShape = null;
                    isDrawing = false;
                    Invalidate();
                }
                else if (isSelecting) { SelectShapesInRect(selectionRect); isSelecting = false; selectionRect = Rectangle.Empty; Invalidate(); }
            }
        }

        private void CanvasControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && isDrawing) { currentShape = null; isDrawing = false; Invalidate(); }
            else if (e.Control) { if (e.KeyCode == Keys.Z) Undo(); else if (e.KeyCode == Keys.Y) Redo(); else if (e.KeyCode == Keys.C) CopySelectedShapes(); else if (e.KeyCode == Keys.V) PasteShapes(); }
            else if (e.KeyCode == Keys.Delete) DeleteSelectedShapes();
            else if (e.KeyCode == Keys.R && selectedShapes.Count > 0) RotateSelectedShapes(15);
            else
            {
                int dx = 0, dy = 0;
                switch (e.KeyCode) { case Keys.Left: dx = -1; break; case Keys.Right: dx = 1; break; case Keys.Up: dy = -1; break; case Keys.Down: dy = 1; break; }
                if (dx != 0 || dy != 0) MoveSelectedShapes(dx, dy);
            }
        }

        private void CanvasControl_MouseWheel(object sender, MouseEventArgs e)
        { if (currentMode == DrawingMode.Select && selectedShapes.Count > 0) { if (e.Delta > 0) RotateSelectedShapes(10); else if (e.Delta < 0) RotateSelectedShapes(-10); } }

        private void SelectShapesInRect(Rectangle rect)
        {
            ClearSelection();
            foreach (var layer in layerManager.Layers)
            {
                if (!layer.Visible) continue;
                foreach (var shape in layer.Shapes)
                    if (rect.IntersectsWith(shape.GetBoundingRect())) { shape.Selected = true; selectedShapes.Add(shape); }
            }
        }
    }
}