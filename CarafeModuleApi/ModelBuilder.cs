using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using CarafeModule;
using CadApplication = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace CarafeModuleApi
{
    /// <summary>
    /// Класс, реализующий построение 3D-модели графина в AutoCAD.
    /// </summary>
    public class ModelBuilder
    {
        /// <summary>
        /// Высота горла графина.
        /// </summary>
        private const double _throatHeight = 10;

        /// <summary>
        /// Толщина графина.
        /// </summary>
        private const double _thicknessObject = 0.5;

        /// <summary>
        /// Длина угол наклона ручки графина.
        /// </summary>
        private const double _arcLength = 5;

        /// <summary>
        /// Параметры графина.
        /// </summary>
        private readonly Parameters _parameters;

        /// <summary>
        /// Активный документ (чертеж) в AutoCAD.
        /// </summary>
        private Document _document;

        /// <summary>
        /// База данных документа.
        /// </summary>
        private Database _database;

        /// <summary>
        /// Поле, хранящее значение радиус основания графина.
        /// </summary>
        private double _baseRadius;

        /// <summary>
        /// Поле, хранящее значение высоту графина.
        /// </summary>
        private double _carafeHeight;

        /// <summary>
        /// Поле, хранящее значение радиус горла графина.
        /// </summary>
        private double _throatRadius;

        /// <summary>
        /// Поле, хранящее значение высоты крышки графина.
        /// </summary>
        private double _stopperHeight;

        /// <summary>
        /// Поля, хранящее значение высоты от основания до высоты горла графина.
        /// </summary>
        private double _heightAboveThroat;

        /// <summary>
        /// Поля, хранящее значение высоты от основания до начала горла графина.
        /// </summary>
        private double _heightToThroat;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parameters">Параметры, необходимые для построения.</param>
        public ModelBuilder(Parameters parameters)
        {
            _parameters = parameters;
            InitialSetting();
            FillParameters();
        }

        /// <summary>
        /// Преднастройка.
        /// </summary>
        private void InitialSetting()
        {
            _document = CadApplication.DocumentManager.MdiActiveDocument;
            _database = _document.Database;

            _database.Insunits = UnitsValue.Millimeters;
        }

        /// <summary>
        /// Заполнение параметров необходимыми значениями.
        /// </summary>
        private void FillParameters()
        {
            _carafeHeight = _parameters.GetValue(ParameterType.CarafeHeight);
            _baseRadius =
                Math.Round(_parameters.GetValue(ParameterType.BaseDiameter) / 2,
                    2);

            _throatRadius =
                Math.Round((_parameters.GetValue(ParameterType.ThroatDiameter) / 2),
                    2);

            _stopperHeight = _parameters.GetValue(ParameterType.StopperHeight);

            _heightToThroat = _carafeHeight - Math.Round(_carafeHeight / 10, 2);
            _heightAboveThroat = _heightToThroat + _throatHeight;
            _stopperHeight += _carafeHeight;
        }

        /// <summary>
        /// Метод, который рисует 3D-модель графина.
        /// </summary>
        public void DrawCarafe()
        {
            _document.LockDocument();

            DrawBase();
            DrawStopper();
            DrawHandle();
            

            SetViewStyle();

        }


        /// <summary>
        /// Метод, который рисует базовую часть графина (без крышки и ручки).
        /// </summary>
        private void DrawBase()
        {
            using (Transaction transaction =
                _database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = GetBlockTableRecord(transaction);
                 //TODO: RSDN
                var minHeightFigure = Math.Round((_carafeHeight / 1.5), 2);

                Polyline acPoly = new Polyline();
                acPoly.SetDatabaseDefaults();
                var index = 0;

                acPoly.AddVertexAt(index++, new Point2d(0, 0), 0, 0, 0);
                acPoly.AddVertexAt(index++, new Point2d(_baseRadius, 
                    0), 0, 0, 0);
                acPoly.AddVertexAt(index++, new Point2d(_baseRadius,
                    minHeightFigure), 0,  0, 0);

                if (_baseRadius != _throatRadius)
                {
                    acPoly.AddVertexAt(index++,
                        new Point2d(_throatRadius, _heightToThroat),
                        0, 0, 0);

                    acPoly.AddVertexAt(index++,
                        new Point2d(_throatRadius, _heightAboveThroat), 0, 0, 0);

                    acPoly.AddVertexAt(index++,
                        new Point2d(_throatRadius - _thicknessObject,
                            _heightAboveThroat),
                        0, 0, 0);

                    acPoly.AddVertexAt(index++,
                        new Point2d(_throatRadius - _thicknessObject,
                            _heightToThroat), 0, 0,
                        0);
                }
                else
                {
                    acPoly.AddVertexAt(index++,
                        new Point2d(_baseRadius, _heightAboveThroat),
                        0, 0, 0);

                    acPoly.AddVertexAt(index++,
                        new Point2d(_baseRadius - _thicknessObject,
                            _heightAboveThroat),
                        0, 0, 0);
                }

                acPoly.AddVertexAt(index++,
                    new Point2d(_baseRadius - _thicknessObject,
                        minHeightFigure), 0,
                    0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(_baseRadius - _thicknessObject,
                        _thicknessObject), 0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(0, _thicknessObject), 0, 0, 0);

                blockTableRecord.AppendEntity(acPoly);

                transaction.Commit();

                Create3DShape(acPoly, 360);
            }
        }

        /// <summary>
        /// Метод, который рисует крышку графина.
        /// </summary>
        private void DrawStopper()
        {
            using (Transaction transaction =
                _database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = GetBlockTableRecord(transaction);

                double innerRadius = Math.Round((_throatRadius / 2), 2);
                double baseStopperRadios = _throatRadius - 0.5;

                Polyline acPoly = new Polyline();

                acPoly.SetDatabaseDefaults();
                var index = 0;
                acPoly.AddVertexAt(index++, new Point2d(0, _heightToThroat), 0, 0, 0);
                acPoly.AddVertexAt(index++,
                    new Point2d(baseStopperRadios, _heightToThroat),
                    0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(baseStopperRadios, _heightAboveThroat), 0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(_throatRadius, _heightAboveThroat), 0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(_throatRadius,
                        _heightAboveThroat + _thicknessObject), 0, 0,
                    0);

                acPoly.AddVertexAt(index++,
                    new Point2d(innerRadius,
                        _heightAboveThroat + _thicknessObject), 0, 0,
                    0);

                acPoly.AddVertexAt(index++,
                    new Point2d(_throatRadius, _stopperHeight),
                    0, 0, 0);

                acPoly.AddVertexAt(index++, new Point2d(0, _stopperHeight),
                    0, 0, 0);

                acPoly.AddVertexAt(index++, new Point2d(0, _heightToThroat),
                    0, 0, 0);


                blockTableRecord.AppendEntity(acPoly);

                transaction.Commit();

                Create3DShape(acPoly, 360);
            }
        }

        /// <summary>
        /// Метод, который рисует ручку графина.
        /// </summary>
        private void DrawHandle()
        {
            using (Transaction transaction =
                _database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = GetBlockTableRecord(transaction);

                double handleAngle = _parameters.GetValue(ParameterType.HandleAngle);
                double handleLength = _parameters.GetValue(ParameterType.HandleLength);

                double handleWidth = _baseRadius;
                double arcValue = (handleAngle * Math.PI) / 180;

                if (handleAngle != 0.0)
                {
                    handleLength -= _arcLength * Math.Sin(arcValue);
                }

                Polyline acPoly = new Polyline();
                acPoly.SetDatabaseDefaults();
                var index = 0;
                acPoly.AddVertexAt(index++, new Point2d(_throatRadius, _heightToThroat),
                    0, 0, 0);

                acPoly.AddVertexAt(index++, new Point2d(handleWidth, _heightToThroat), 0,
                    0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(
                        handleWidth + _arcLength * Math.Cos(arcValue),
                        _heightToThroat -
                        _arcLength * Math.Sin(arcValue)),
                    0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(
                        handleWidth + _arcLength * Math.Cos(arcValue),
                        _heightToThroat -
                        _arcLength * Math.Sin(arcValue) - handleLength),
                    0, 0, 0);


                acPoly.AddVertexAt(index++,
                    new Point2d(
                        handleWidth + _arcLength * Math.Cos(arcValue) +
                        _thicknessObject,
                        _heightToThroat -
                        _arcLength * Math.Sin(arcValue) - handleLength),
                    0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(
                        handleWidth + _arcLength * Math.Cos(arcValue) +
                        _thicknessObject,
                        _heightToThroat -
                        _arcLength * Math.Sin(arcValue) +
                        _thicknessObject),
                    0, 0, 0);


                acPoly.AddVertexAt(index++,
                    new Point2d(handleWidth + _thicknessObject,
                        _heightToThroat + _thicknessObject),
                    0, 0, 0);

                acPoly.AddVertexAt(index++,
                    new Point2d(_throatRadius,
                        _heightToThroat + _thicknessObject), 0, 0, 0);

                acPoly.AddVertexAt(index++, new Point2d(_throatRadius, _heightToThroat),
                    0, 0, 0);


                blockTableRecord.AppendEntity(acPoly);

                transaction.Commit();

                Create3DShape(acPoly, -30);
            }
        }

        /// <summary>
        /// Метод, который строит 3D-модель фигуры.
        /// </summary>
        /// <param name="figure">Фигура, которую необходимо построить.</param>
        /// <param name="angleOfRevolution">Угол поворота.</param>
        private void Create3DShape(DBObject figure, double angleOfRevolution)
        {
            using (Transaction transaction =
                _database.TransactionManager.StartTransaction())
            {
                double angleRevolution = (angleOfRevolution * Math.PI) / 180;
                var blockTableRecord = GetBlockTableRecord(transaction);

                Line axisLine = new Line(new Point3d(0, 0, 0),
                    new Point3d(0, _parameters.GetValue(ParameterType.CarafeHeight), 0));

                RevolveOptionsBuilder revolveOptionsBuilder =
                    new RevolveOptionsBuilder
                    {
                        CloseToAxis = false, DraftAngle = 0, TwistAngle = 0
                    };


                using (Solid3d acSol3D = new Solid3d())
                {
                    Entity prof =
                        (Entity) transaction.GetObject(figure.ObjectId,
                            OpenMode.ForRead);

                    acSol3D.CreateRevolvedSolid(prof, axisLine.StartPoint,
                        axisLine.EndPoint.GetAsVector() -
                        axisLine.StartPoint.GetAsVector(),
                        angleRevolution, 0, revolveOptionsBuilder.ToRevolveOptions());

                    blockTableRecord.AppendEntity(acSol3D);
                    transaction.AddNewlyCreatedDBObject(acSol3D, true);
                }

                transaction.Commit();
            }
        }
        
        /// <summary>
        /// Изменение визуального стиля в текущем видовом экране на 'Концептуальный'.
        /// </summary>
        private void SetViewStyle()
        {
            using (Transaction transaction =
                _database.TransactionManager.StartTransaction())
            {
                ViewportTable vt =
                    (ViewportTable) transaction.GetObject(
                        _database.ViewportTableId, OpenMode.ForRead
                    );
                ViewportTableRecord vtr =
                    (ViewportTableRecord) transaction.GetObject(
                        vt["*Active"], OpenMode.ForWrite
                    );
                DBDictionary dict =
                    (DBDictionary) transaction.GetObject(
                        _database.VisualStyleDictionaryId, OpenMode.ForRead
                    );

                vtr.VisualStyleId = dict.GetAt("Conceptual");
                transaction.Commit();
            }

            _document.Editor.UpdateTiledViewportsFromDatabase();
        }

        /// <summary>
        /// Метод, возвращающий таблицу блоков записей.
        /// </summary>
        /// <param name="transaction">Текущая транзакция.</param>
        /// <returns>Таблица блоков записей.</returns>
        private BlockTableRecord GetBlockTableRecord(Transaction transaction)
        {
            var blockTable =
                transaction.GetObject(_database.BlockTableId, OpenMode.ForRead) as
                    BlockTable;

            return transaction.GetObject(blockTable[BlockTableRecord.ModelSpace],
                OpenMode.ForWrite) as BlockTableRecord;
        }
    }
}