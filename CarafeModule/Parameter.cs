using System;

namespace CarafeModule
{
    /// <summary>
    /// Класс, хранящий информацию о параметрах графина.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Значение параметра.
        /// </summary>
        private double _value;

        /// <summary>
        /// Максимально допустимое значение параметра.
        /// </summary>
        private double _maxValue = 0;

        /// <summary>
        /// Получить / задать максимально допустимое значение параметра.
        /// </summary>
        public double MaxValue
        {
            get => _maxValue;
            set => _maxValue = value < MinValue ? MinValue : value;
        }

        /// <summary>
        /// Получить минимально допустимое значение параметра.
        /// </summary>
        public double MinValue { get; private set; } = 0;

        /// <summary>
        /// Получить / задать текущее значение параметра.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value >= MaxValue)
                {
                    _value = MaxValue;
                }
                else if (value <= MinValue)
                {
                    _value = MinValue;
                }
                else
                {
                    _value = value;
                }
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="minValue">Минимально допустимное значение параметра.</param>
        /// <param name="maxValue">Максимально допустимное значение параметра.</param>
        /// <param name="value">Текущее значение параметра.</param>
        public Parameter(double minValue,
            double maxValue,
            double value)
        {
            if (minValue > maxValue)
            {
                MinValue = maxValue;
                MaxValue = minValue;
            }
            else
            {
                MinValue = minValue;
                MaxValue = maxValue;
            }

            Value = value;
        }

        //TODO: XML
        /// <summary>
        /// Проверка текущих пораметорв.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() != this.GetType())
            {
                return false;
            }

            Parameter parameter = (Parameter) obj;
            return (this.MinValue == parameter.MinValue &&
                    this.MaxValue == parameter.MaxValue && this.Value == parameter.Value);
        }
    }
}