using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CarafeModule;

namespace CarafeModuleUI
{
    /// <summary>
    /// Главная форма приложения.
    /// </summary>
    public partial class CarafeForm : Form
    {
        /// <summary>
        /// Поле, хранящее параметры и их значения.
        /// </summary>
        private readonly Parameters _parameters;

        /// <summary>
        /// Поле, хранящее название TextBox и соответствующуе ему тип параметра.
        /// </summary>
        private Dictionary<TextBox, ParameterType> _fields;

        /// <summary>
        /// Поле, хранящее тип параметра и соответствующее ему элемент Label.
        /// </summary>
        private Dictionary<ParameterType, Label> _labels;

        /// <summary>
        /// Получить значения поля _parameters.
        /// </summary>
        public Parameters Parameters
        {
            get => _parameters;
        }

        //TODO: Не используется?
        /// <summary>
        /// Задать возможность редактирования полей HandleAngleTextBox и HandleLengthTextBox.
        /// </summary>
        private bool IsHasHandle
        {
            set
            {
                HandleAngleTextBox.Enabled = value;
                HandleLengthTextBox.Enabled = value;

                _parameters.HandleState =
                    value ? ParameterState.Present : ParameterState.Missing;
            }
        }

        //TODO: Не используется?
        /// <summary>
        /// Задать возможность редактирования поля StopperHeightTextBox.
        /// </summary>
        private bool IsHasBottleStopper
        {
            set
            {
                StopperHeightTextBox.Enabled = value;

                _parameters.StopperState =
                    value ? ParameterState.Present : ParameterState.Missing;
            }
        }

        /// <summary>
        /// Инициализация полей TextBox.
        /// </summary>
        private void InitializeFields()
        {
            _fields = new Dictionary<TextBox, ParameterType>
            {
                {BaseDiameterTextBox, ParameterType.BaseDiameter},
                {CarafeHeightTextBox, ParameterType.CarafeHeight},
                {ThroatDiameterTextBox, ParameterType.ThroatDiameter},
                {StopperHeightTextBox, ParameterType.StopperHeight},
                {HandleAngleTextBox, ParameterType.HandleAngle},
                {HandleLengthTextBox, ParameterType.HandleLength}
            };
        }

        /// <summary>
        /// Инициализация полей Label.
        /// </summary>
        private void InitializeLabels()
        {
            _labels = new Dictionary<ParameterType, Label>
            {
                {ParameterType.BaseDiameter, RangeBaseDiameterLabel},
                {ParameterType.CarafeHeight, RangeCarafeHeightLabel},
                {ParameterType.ThroatDiameter, RangeThroatDiameterLabel},
                {ParameterType.StopperHeight, RangeStopperHeightLabel},
                {ParameterType.HandleLength, RangeHandleLengthLabel},
                {ParameterType.HandleAngle, RangeHandleAngleLabel}
            };
        }

        /// <summary>
        /// Заполнение label информацией о допустимом диапазоне чисел.
        /// </summary>
        private void UpdateValueLabels()
        {
            foreach (var keyValues in _labels)
            {
                keyValues.Value.Text = "(min: " +
                                       Convert.ToString(
                                           _parameters.GetMinValue(keyValues.Key)) +
                                       " | max: " +
                                       Convert.ToString(
                                           _parameters.GetMaxValue(keyValues.Key)) +
                                       " mm)";
            }
        }

        /// <summary>
        /// Метод, который задает значение для соответствующее TextBox-а.
        /// </summary>
        /// <param name="textBox">Элемент, в который нужно вставить значение.</param>
        private void SetValueInTextBox(TextBox textBox)
        {
            textBox.Text =
                Convert.ToString(_parameters.GetValue(_fields[textBox]));
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public CarafeForm()
        {
            InitializeComponent();

            _parameters = new Parameters();

            InitializeFields();
            InitializeLabels();

            
            UpdateValueLabels();
        }

     
        /// <summary>
        /// Событие, срабатываемое при покидании TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        SetValueInTextBox(textBox);
                        return;
                    }

                    if (!double.TryParse(textBox.Text, out double resultValue))
                    {
                        SetValueInTextBox(textBox);
                        return;
                    }

                    if (!_parameters.IsCorrectValue(_fields[textBox], resultValue))
                    {
                        SetValueInTextBox(textBox);
                        return;
                    }

                    _parameters.SetValue(_fields[textBox], resultValue);
                    SetValueInTextBox(textBox);

                  

                    UpdateValueLabels();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, которое обрабатывает нажатие клавиши в поле TextBox.
        /// Позволяет вводить только цифры и управляющие символы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyPressOnlyNumber(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                e.KeyChar = e.KeyChar == ',' ? e.KeyChar = '.' : e.KeyChar;

                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    return;
                }

                if (e.KeyChar == '.')
                {
                    e.Handled = textBox.Text.Contains(".")
                        ? e.Handled = true
                        : e.Handled = false;

                    return;
                }

                if (char.IsDigit(e.KeyChar))
                {
                    int maxIntLength = 2;
                    int maxDoubleLength = 1;
                    if (!textBox.Text.Contains(".") && textBox.Text.Length > maxIntLength)
                    {
                        e.Handled = true;
                        return;
                    }

                    if (textBox.Text.Contains("."))
                    {
                        var indexSubstring = textBox.Text.IndexOf('.') + 1;
                        var substring = textBox.Text.Substring(indexSubstring);

                        e.Handled = substring.Length > maxDoubleLength
                            ? e.Handled = true
                            : e.Handled = false;

                        return;
                    }

                    e.Handled = false;
                    return;
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Событие, которое срабатывает, когда TextBox становится активным.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = "";
            }
        }

        /// <summary>
        /// Действие, срабатываемое при нажатии на кнопку Build.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Действие, срабатываемое при нажатии на кнопку Cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}