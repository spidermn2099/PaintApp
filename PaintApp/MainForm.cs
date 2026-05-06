namespace PaintApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.canvas = new PaintApp.Controls.CanvasControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile = new System.Windows.Forms.ToolStripSeparator();
            this.loadPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorPlugin = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rotate90ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapeToolStrip = new System.Windows.Forms.ToolStrip();
            this.ddbShapes = new System.Windows.Forms.ToolStripDropDownButton();
            this.отрезокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ломанаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прямоугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.эллипсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.многоугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSegment = new System.Windows.Forms.ToolStripButton();
            this.btnPolyline = new System.Windows.Forms.ToolStripButton();
            this.btnEllipse = new System.Windows.Forms.ToolStripButton();
            this.btnRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnPolygon = new System.Windows.Forms.ToolStripButton();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.propertyToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbThickness = new System.Windows.Forms.ToolStripComboBox();
            this.btnLineColor = new System.Windows.Forms.ToolStripButton();
            this.btnFillColor = new System.Windows.Forms.ToolStripButton();
            this.editToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotateLeft = new System.Windows.Forms.ToolStripButton();
            this.btnRotateRight = new System.Windows.Forms.ToolStripButton();
            this.dockPanel = new System.Windows.Forms.Panel();
            this.btnMoveToLayer = new System.Windows.Forms.Button();
            this.btnLayerDown = new System.Windows.Forms.Button();
            this.btnLayerUp = new System.Windows.Forms.Button();
            this.btnRemoveLayer = new System.Windows.Forms.Button();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.cmbCurrentLayer = new System.Windows.Forms.ComboBox();
            this.lstLayers = new System.Windows.Forms.ListBox();
            this.labelLayers = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.shapeToolStrip.SuspendLayout();
            this.propertyToolStrip.SuspendLayout();
            this.editToolStrip.SuspendLayout();
            this.dockPanel.SuspendLayout();
            this.SuspendLayout();

            // canvas
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 75);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(984, 496);
            this.canvas.TabIndex = 0;

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";

            // fileToolStripMenuItem
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparatorFile,
            this.loadPluginToolStripMenuItem,
            this.toolStripSeparatorPlugin,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";

            // openToolStripMenuItem
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openToolStripMenuItem.Text = "Открыть...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);

            // saveToolStripMenuItem
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "Сохранить...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);

            // toolStripSeparatorFile
            this.toolStripSeparatorFile.Name = "toolStripSeparatorFile";
            this.toolStripSeparatorFile.Size = new System.Drawing.Size(152, 6);

            // loadPluginToolStripMenuItem
            this.loadPluginToolStripMenuItem.Name = "loadPluginToolStripMenuItem";
            this.loadPluginToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadPluginToolStripMenuItem.Text = "Загрузить плагин...";
            this.loadPluginToolStripMenuItem.Click += new System.EventHandler(this.loadPluginToolStripMenuItem_Click);

            // toolStripSeparatorPlugin
            this.toolStripSeparatorPlugin.Name = "toolStripSeparatorPlugin";
            this.toolStripSeparatorPlugin.Size = new System.Drawing.Size(152, 6);

            // exitToolStripMenuItem
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);

            // editToolStripMenuItem
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem,
            this.deleteAllToolStripMenuItem,
            this.toolStripSeparator3,
            this.rotate90ToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.editToolStripMenuItem.Text = "Правка";

            // undoToolStripMenuItem
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.undoToolStripMenuItem.Text = "Отменить";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);

            // redoToolStripMenuItem
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.redoToolStripMenuItem.Text = "Повторить";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);

            // toolStripSeparator1
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);

            // copyToolStripMenuItem
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.copyToolStripMenuItem.Text = "Копировать";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);

            // pasteToolStripMenuItem
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.pasteToolStripMenuItem.Text = "Вставить";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);

            // toolStripSeparator2
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);

            // deleteToolStripMenuItem
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.deleteToolStripMenuItem.Text = "Удалить";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);

            // deleteAllToolStripMenuItem
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.deleteAllToolStripMenuItem.Text = "Удалить всё";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click);

            // toolStripSeparator3
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);

            // rotate90ToolStripMenuItem
            this.rotate90ToolStripMenuItem.Name = "rotate90ToolStripMenuItem";
            this.rotate90ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.rotate90ToolStripMenuItem.Text = "Повернуть на 90°";
            this.rotate90ToolStripMenuItem.Click += new System.EventHandler(this.rotate90ToolStripMenuItem_Click);

            // helpToolStripMenuItem
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpToolStripMenuItem.Text = "Справка";

            // aboutToolStripMenuItem
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);

            // shapeToolStrip
            this.shapeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddbShapes,
            this.btnSegment,
            this.btnPolyline,
            this.btnEllipse,
            this.btnRectangle,
            this.btnPolygon,
            this.btnSelect});
            this.shapeToolStrip.Location = new System.Drawing.Point(0, 24);
            this.shapeToolStrip.Name = "shapeToolStrip";
            this.shapeToolStrip.Size = new System.Drawing.Size(984, 25);
            this.shapeToolStrip.TabIndex = 2;
            this.shapeToolStrip.Text = "shapeToolStrip";

            // ddbShapes
            this.ddbShapes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ddbShapes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отрезокToolStripMenuItem,
            this.ломанаяToolStripMenuItem,
            this.прямоугольникToolStripMenuItem,
            this.эллипсToolStripMenuItem,
            this.многоугольникToolStripMenuItem});
            this.ddbShapes.Name = "ddbShapes";
            this.ddbShapes.Size = new System.Drawing.Size(63, 22);
            this.ddbShapes.Text = "Фигуры";

            // отрезокToolStripMenuItem
            this.отрезокToolStripMenuItem.Name = "отрезокToolStripMenuItem";
            this.отрезокToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.отрезокToolStripMenuItem.Text = "Отрезок";
            this.отрезокToolStripMenuItem.Click += new System.EventHandler(this.btnSegment_Click);

            // ломанаяToolStripMenuItem
            this.ломанаяToolStripMenuItem.Name = "ломанаяToolStripMenuItem";
            this.ломанаяToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.ломанаяToolStripMenuItem.Text = "Ломаная";
            this.ломанаяToolStripMenuItem.Click += new System.EventHandler(this.btnPolyline_Click);

            // прямоугольникToolStripMenuItem
            this.прямоугольникToolStripMenuItem.Name = "прямоугольникToolStripMenuItem";
            this.прямоугольникToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.прямоугольникToolStripMenuItem.Text = "Прямоугольник";
            this.прямоугольникToolStripMenuItem.Click += new System.EventHandler(this.btnRectangle_Click);

            // эллипсToolStripMenuItem
            this.эллипсToolStripMenuItem.Name = "эллипсToolStripMenuItem";
            this.эллипсToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.эллипсToolStripMenuItem.Text = "Эллипс";
            this.эллипсToolStripMenuItem.Click += new System.EventHandler(this.btnEllipse_Click);

            // многоугольникToolStripMenuItem
            this.многоугольникToolStripMenuItem.Name = "многоугольникToolStripMenuItem";
            this.многоугольникToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.многоугольникToolStripMenuItem.Text = "Шестиугольник";
            this.многоугольникToolStripMenuItem.Click += new System.EventHandler(this.btnPolygon_Click);

            // btnSegment
            this.btnSegment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSegment.Name = "btnSegment";
            this.btnSegment.Size = new System.Drawing.Size(55, 22);
            this.btnSegment.Text = "Отрезок";
            this.btnSegment.Visible = false;
            this.btnSegment.Click += new System.EventHandler(this.btnSegment_Click);

            // btnPolyline
            this.btnPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPolyline.Name = "btnPolyline";
            this.btnPolyline.Size = new System.Drawing.Size(59, 22);
            this.btnPolyline.Text = "Ломаная";
            this.btnPolyline.Visible = false;
            this.btnPolyline.Click += new System.EventHandler(this.btnPolyline_Click);

            // btnEllipse
            this.btnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(54, 22);
            this.btnEllipse.Text = "Эллипс";
            this.btnEllipse.Visible = false;
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);

            // btnRectangle
            this.btnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(83, 22);
            this.btnRectangle.Text = "Прямоугольник";
            this.btnRectangle.Visible = false;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);

            // btnPolygon
            this.btnPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(81, 22);
            this.btnPolygon.Text = "Шестиугольник";
            this.btnPolygon.Visible = false;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);

            // btnSelect
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(67, 22);
            this.btnSelect.Text = "Выделение";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);

            // propertyToolStrip
            this.propertyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbThickness,
            this.btnLineColor,
            this.btnFillColor});
            this.propertyToolStrip.Location = new System.Drawing.Point(0, 49);
            this.propertyToolStrip.Name = "propertyToolStrip";
            this.propertyToolStrip.Size = new System.Drawing.Size(984, 25);
            this.propertyToolStrip.TabIndex = 3;
            this.propertyToolStrip.Text = "propertyToolStrip";

            // toolStripLabel1
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel1.Text = "Толщина:";

            // cmbThickness
            this.cmbThickness.Name = "cmbThickness";
            this.cmbThickness.Size = new System.Drawing.Size(75, 25);
            this.cmbThickness.SelectedIndexChanged += new System.EventHandler(this.cmbThickness_SelectedIndexChanged);

            // btnLineColor
            this.btnLineColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLineColor.Name = "btnLineColor";
            this.btnLineColor.Size = new System.Drawing.Size(83, 22);
            this.btnLineColor.Text = "Цвет линии";
            this.btnLineColor.Click += new System.EventHandler(this.btnLineColor_Click);

            // btnFillColor
            this.btnFillColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(83, 22);
            this.btnFillColor.Text = "Цвет заливки";
            this.btnFillColor.Click += new System.EventHandler(this.btnFillColor_Click);

            // editToolStrip
            this.editToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator4,
            this.btnCopy,
            this.btnPaste,
            this.toolStripSeparator5,
            this.btnDelete,
            this.btnDeleteAll,
            this.toolStripSeparator6,
            this.btnRotateLeft,
            this.btnRotateRight});
            this.editToolStrip.Location = new System.Drawing.Point(0, 74);
            this.editToolStrip.Name = "editToolStrip";
            this.editToolStrip.Size = new System.Drawing.Size(984, 25);
            this.editToolStrip.TabIndex = 4;
            this.editToolStrip.Text = "editToolStrip";

            // btnUndo
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(62, 22);
            this.btnUndo.Text = "Отменить";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);

            // btnRedo
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(65, 22);
            this.btnRedo.Text = "Повторить";
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);

            // toolStripSeparator4
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);

            // btnCopy
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(67, 22);
            this.btnCopy.Text = "Копировать";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);

            // btnPaste
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(57, 22);
            this.btnPaste.Text = "Вставить";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);

            // toolStripSeparator5
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);

            // btnDelete
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(54, 22);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnDeleteAll
            this.btnDeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(73, 22);
            this.btnDeleteAll.Text = "Удалить всё";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);

            // toolStripSeparator6
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);

            // btnRotateLeft
            this.btnRotateLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(31, 22);
            this.btnRotateLeft.Text = "-15°";
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);

            // btnRotateRight
            this.btnRotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(31, 22);
            this.btnRotateRight.Text = "+15°";
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);

            // dockPanel
            this.dockPanel.Controls.Add(this.btnMoveToLayer);
            this.dockPanel.Controls.Add(this.btnLayerDown);
            this.dockPanel.Controls.Add(this.btnLayerUp);
            this.dockPanel.Controls.Add(this.btnRemoveLayer);
            this.dockPanel.Controls.Add(this.btnAddLayer);
            this.dockPanel.Controls.Add(this.cmbCurrentLayer);
            this.dockPanel.Controls.Add(this.lstLayers);
            this.dockPanel.Controls.Add(this.labelLayers);
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockPanel.Location = new System.Drawing.Point(984, 75);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(0, 496);
            this.dockPanel.TabIndex = 5;

            // btnMoveToLayer
            this.btnMoveToLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMoveToLayer.Location = new System.Drawing.Point(0, 218);
            this.btnMoveToLayer.Name = "btnMoveToLayer";
            this.btnMoveToLayer.Size = new System.Drawing.Size(0, 30);
            this.btnMoveToLayer.TabIndex = 7;
            this.btnMoveToLayer.Text = "Переместить в слой";
            this.btnMoveToLayer.UseVisualStyleBackColor = true;
            this.btnMoveToLayer.Click += new System.EventHandler(this.btnMoveToLayer_Click);

            // btnLayerDown
            this.btnLayerDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLayerDown.Location = new System.Drawing.Point(0, 188);
            this.btnLayerDown.Name = "btnLayerDown";
            this.btnLayerDown.Size = new System.Drawing.Size(0, 30);
            this.btnLayerDown.TabIndex = 6;
            this.btnLayerDown.Text = "↓ Вниз";
            this.btnLayerDown.UseVisualStyleBackColor = true;
            this.btnLayerDown.Click += new System.EventHandler(this.btnLayerDown_Click);

            // btnLayerUp
            this.btnLayerUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLayerUp.Location = new System.Drawing.Point(0, 158);
            this.btnLayerUp.Name = "btnLayerUp";
            this.btnLayerUp.Size = new System.Drawing.Size(0, 30);
            this.btnLayerUp.TabIndex = 5;
            this.btnLayerUp.Text = "↑ Вверх";
            this.btnLayerUp.UseVisualStyleBackColor = true;
            this.btnLayerUp.Click += new System.EventHandler(this.btnLayerUp_Click);

            // btnRemoveLayer
            this.btnRemoveLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRemoveLayer.Location = new System.Drawing.Point(0, 128);
            this.btnRemoveLayer.Name = "btnRemoveLayer";
            this.btnRemoveLayer.Size = new System.Drawing.Size(0, 30);
            this.btnRemoveLayer.TabIndex = 4;
            this.btnRemoveLayer.Text = "❌ Удалить слой";
            this.btnRemoveLayer.UseVisualStyleBackColor = true;
            this.btnRemoveLayer.Click += new System.EventHandler(this.btnRemoveLayer_Click);

            // btnAddLayer
            this.btnAddLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddLayer.Location = new System.Drawing.Point(0, 98);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(0, 30);
            this.btnAddLayer.TabIndex = 3;
            this.btnAddLayer.Text = "➕ Создать слой";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);

            // cmbCurrentLayer
            this.cmbCurrentLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbCurrentLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentLayer.Location = new System.Drawing.Point(0, 75);
            this.cmbCurrentLayer.Name = "cmbCurrentLayer";
            this.cmbCurrentLayer.Size = new System.Drawing.Size(0, 21);
            this.cmbCurrentLayer.TabIndex = 2;
            this.cmbCurrentLayer.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentLayer_SelectedIndexChanged);

            // lstLayers
            this.lstLayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstLayers.FormattingEnabled = true;
            this.lstLayers.Location = new System.Drawing.Point(0, 23);
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.Size = new System.Drawing.Size(0, 52);
            this.lstLayers.TabIndex = 1;
            this.lstLayers.SelectedIndexChanged += new System.EventHandler(this.lstLayers_SelectedIndexChanged);

            // labelLayers
            this.labelLayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLayers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelLayers.Location = new System.Drawing.Point(0, 0);
            this.labelLayers.Name = "labelLayers";
            this.labelLayers.Size = new System.Drawing.Size(0, 23);
            this.labelLayers.TabIndex = 0;
            this.labelLayers.Text = "Слои";
            this.labelLayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 571);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.editToolStrip);
            this.Controls.Add(this.propertyToolStrip);
            this.Controls.Add(this.shapeToolStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Paint - Графический редактор";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.shapeToolStrip.ResumeLayout(false);
            this.shapeToolStrip.PerformLayout();
            this.propertyToolStrip.ResumeLayout(false);
            this.propertyToolStrip.PerformLayout();
            this.editToolStrip.ResumeLayout(false);
            this.editToolStrip.PerformLayout();
            this.dockPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Поля формы
        private PaintApp.Controls.CanvasControl canvas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFile;
        private System.Windows.Forms.ToolStripMenuItem loadPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorPlugin;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem rotate90ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip shapeToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton ddbShapes;
        private System.Windows.Forms.ToolStripMenuItem отрезокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ломанаяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прямоугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem эллипсToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem многоугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnSegment;
        private System.Windows.Forms.ToolStripButton btnPolyline;
        private System.Windows.Forms.ToolStripButton btnEllipse;
        private System.Windows.Forms.ToolStripButton btnRectangle;
        private System.Windows.Forms.ToolStripButton btnPolygon;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStrip propertyToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbThickness;
        private System.Windows.Forms.ToolStripButton btnLineColor;
        private System.Windows.Forms.ToolStripButton btnFillColor;
        private System.Windows.Forms.ToolStrip editToolStrip;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnDeleteAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnRotateLeft;
        private System.Windows.Forms.ToolStripButton btnRotateRight;
        private System.Windows.Forms.Panel dockPanel;
        private System.Windows.Forms.Button btnMoveToLayer;
        private System.Windows.Forms.Button btnLayerDown;
        private System.Windows.Forms.Button btnLayerUp;
        private System.Windows.Forms.Button btnRemoveLayer;
        private System.Windows.Forms.Button btnAddLayer;
        private System.Windows.Forms.ComboBox cmbCurrentLayer;
        private System.Windows.Forms.ListBox lstLayers;
        private System.Windows.Forms.Label labelLayers;
    }
}