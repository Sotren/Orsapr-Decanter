using System;
using System.Collections.Generic;

namespace CarafeModule
{
    /// <summary>
    /// Класс, для хранения данных о параметрах.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Минимальное значение высоты графина.
        /// </summary>
        public const double MinCarafeHeight = 100;

        /// <summary>
        /// Максимальное значение высоты графина.
        /// </summary>
        public const double MaxCarafeHeight = 300;

        /// <summary>
        /// Минимальное значение диаметра горла графина.
        /// </summary>
        public const double MinThroatDiameter = 25;

        /// <summary>
        /// Минимальное значение диаметра основания графина.
        /// </summary>
        public const double MinBaseDiameter = 50;

        /// <summary>
        /// Максимальное значение диаметра основания графина.
        /// </summary>
        public const double MaxBaseDiameter = 100;

        /// <summary>
        /// Минимальное значение высоты крышки графина.
        /// </summary>
        public const double MinStopperHeight = 10;

        /// <summary>
        /// Максимальное значение высоты крышки графина.
        /// </summary>
        public const double MaxStopperHeight = 50;

        /// <summary>
        /// Минимальное значение длины ручки графина.
        /// </summary>
        public const double MinHandleLength = 25;

        /// <summary>
        /// Минимальное значение угла ручки графина.
        /// </summary>
        public const double MinHandleAngle = 0;

        /// <summary>
        /// Максимальное значение угла ручки графина.
        /// </summary>
        public const double MaxHandleAngle = 90;

        /// <summary>
        /// Словарь, хранящий тип параметра и его характеристики.
        /// </summary>
        private Dictionary<ParameterType, Parameter> _parameters;

        /// <summary>
        /// Метод, который обновляет максимально допустимое значение для длины ручки графина.
        /// </summary>
        private void UpdateMaxHandleLength()
        {
            _parameters[ParameterType.HandleLength].MaxValue =
                Math.Round((2 * _parameters[ParameterType.CarafeHeight].Value) / 3, 2);
        }

        /// <summary>
        /// Метод, который обновляет максимально допустимое значение для диаметра горла графина.
        /// </summary>
        private void UpdateMaxThroatDiameter()
        {
            _parameters[ParameterType.ThroatDiameter].MaxValue =
                _parameters[ParameterType.BaseDiameter].Value;
        }

        /// <summary>
        /// Метод корректирует текущее значение в допустимом диапазоне параметра.
        /// </summary>
        /// <param name="parameterType">Параметр.</param>
        private void AdjustmentValueParameter(ParameterType parameterType)
        {
         
            if (_parameters[parameterType].MaxValue <
                _parameters[parameterType].Value)
            {
                _parameters[parameterType].Value =
                    _parameters[parameterType].MaxValue;
            }
            else if (_parameters[parameterType].Value <
                     _parameters[parameterType].MinValue)
            {
                _parameters[parameterType].Value =
                    _parameters[parameterType].MinValue;
            }
        }
       
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Parameters()
        {
            double maxThroatDiameter =
                MinBaseDiameter;

            double maxHandleLength =
                Math.Round((double) ((2 * MinCarafeHeight) / 3), 2);

            _parameters =
                new Dictionary<ParameterType, Parameter>
                {
                    {
                        ParameterType.BaseDiameter, new Parameter(
                            MinBaseDiameter,
                            MaxBaseDiameter,
                            MinBaseDiameter)
                    },
                    {
                        ParameterType.CarafeHeight, new Parameter(
                            MinCarafeHeight,
                            MaxCarafeHeight,
                            MinCarafeHeight)
                    },
                    {
                        ParameterType.ThroatDiameter, new Parameter(
                            MinThroatDiameter, maxThroatDiameter,
                            MinThroatDiameter)
                    },
                    {
                        ParameterType.StopperHeight, new Parameter(
                            MinStopperHeight,
                            MaxStopperHeight,
                            MinStopperHeight)
                    },
                    {
                        ParameterType.HandleAngle, new Parameter(
                            MinHandleAngle,
                            MaxHandleAngle,
                            MinHandleAngle)
                    },
                    {
                        ParameterType.HandleLength, new Parameter(
                            MinHandleLength, maxHandleLength,
                            MinHandleLength)
                    }
                };
        }

        /// <summary>
        /// Получить минимально допустимое значение параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <returns>Минимально допустимое значение параметра.</returns>
        public double GetMinValue(ParameterType parameterType)
        {
            return _parameters[parameterType].MinValue;
        }

        /// <summary>
        /// Получить максимально допустимое значение параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра</param>
        /// <returns>Максимально допустимое значение параметра.</returns>
        public double GetMaxValue(ParameterType parameterType)
        {
            return _parameters[parameterType].MaxValue;
        }

        /// <summary>
        /// Получить значение параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <returns>Значение параметра.</returns>
        public double GetValue(ParameterType parameterType)
        {
            return _parameters[parameterType].Value;
        }

        /// <summary>
        /// Задать значение параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="newValue">Новое значение параметра.</param>
        public void SetValue(ParameterType parameterType, double newValue)
        {
            if (newValue >= _parameters[parameterType].MinValue &&
                newValue <= _parameters[parameterType].MaxValue)
            {
                _parameters[parameterType].Value = newValue;
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "Значение : " + newValue +
                    " не входит в диапазон допустимых значений для параметра " +
                    parameterType);
            }

            if (parameterType == ParameterType.BaseDiameter)
            {
                UpdateMaxThroatDiameter();
                AdjustmentValueParameter(ParameterType.ThroatDiameter);
            }

            if (parameterType == ParameterType.CarafeHeight)
            {
                UpdateMaxHandleLength();
                AdjustmentValueParameter(ParameterType.HandleLength);
            }
        }

        /// <summary>
        /// Проверка полученного числа.
        /// </summary>
        /// <param name="parameterType">Название параметра, значение которого проверяется.</param>
        /// <param name="value">Само значение параметра.</param>
        /// <returns>Возвращает результат, входит ли число в диапазон допустимых значений.</returns>
        public bool IsCorrectValue(ParameterType parameterType, double value)
        {
            return !(value > _parameters[parameterType].MaxValue) &&
                   !(value < _parameters[parameterType].MinValue);
        }
    }
}