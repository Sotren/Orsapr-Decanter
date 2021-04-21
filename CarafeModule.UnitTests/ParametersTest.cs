using System;
using NUnit.Framework;

namespace CarafeModule.UnitTests
{
    /// <summary>
    /// Тестирование класса Parameters.
    /// </summary>
    [TestFixture(Description = "Модульные тесты класса Parameters.")]
    public class ParametersTest
    {
        private Parameters _parameters;

        [SetUp]
        public void CreateParameters()
        {
            _parameters = new Parameters();
        }

        [TestCase(ParameterType.BaseDiameter, Parameters.MinBaseDiameter,
            "Метод возвращает некорректное минимальное значение параметра BaseDiameter.",
            TestName =
                "Тест метода GetMinValue: Получить минимальное значение параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.ThroatDiameter, Parameters.MinThroatDiameter,
            "Метод возвращает некорректное минимальное значение параметра ThroatDiameter.",
            TestName =
                "Тест метода GetMinValue: Получить минимальное значение параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, Parameters.MinCarafeHeight,
            "Метод возвращает некорректное минимальное значение параметра CarafeHeight.",
            TestName =
                "Тест метода GetMinValue: Получить минимальное значение параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.HandleLength, Parameters.MinHandleLength,
            "Метод возвращает некорректное минимальное значение параметра HandleLength.",
            TestName =
                "Тест метода GetMinValue: Получить минимальное значение параметра 'HandleLength'.")]
        [TestCase(ParameterType.StopperHeight, Parameters.MinStopperHeight,
            "Метод возвращает некорректное минимальное значение параметра StopperHeight.",
            TestName =
                "Тест метода GetMinValue: Получить минимальное значение параметра 'StopperHeight'.")]
        [TestCase(ParameterType.HandleAngle, Parameters.MinHandleAngle,
            "Метод возвращает некорректное минимальное значение параметра HandleAngle.",
            TestName =
                "Тест метода GetMinValue: Получить минимальное значение параметра 'HandleAngle'.")]
        public void Test_GetMinValue_CorrectValue(ParameterType parameterType,
            double expectedValue, string message)
        {
            var actualValue = _parameters.GetMinValue(parameterType);
            Assert.AreEqual(expectedValue, actualValue, message);
        }

        [TestCase(ParameterType.BaseDiameter, Parameters.MaxBaseDiameter,
            "Метод возвращает некорректное максимальное значение параметра BaseDiameter.",
            TestName =
                "Тест метода GetMinValue: Получить максимальное значение параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, Parameters.MaxCarafeHeight,
            "Метод возвращает некорректное максимальное значение параметра CarafeHeight.",
            TestName =
                "Тест метода GetMinValue: Получить максимальное значение параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.StopperHeight, Parameters.MaxStopperHeight,
            "Метод возвращает некорректное максимальное значение параметра StopperHeight.",
            TestName =
                "Тест метода GetMinValue: Получить максимальное значение параметра 'StopperHeight'.")]
        [TestCase(ParameterType.HandleAngle, Parameters.MaxHandleAngle,
            "Метод возвращает некорректное максимальное значение параметра HandleAngle.",
            TestName =
                "Тест метода GetMinValue: Получить максимальное значение параметра 'HandleAngle'.")]
        public void Test_GetMaxValue_CorrectValue(ParameterType parameterType,
            double expectedValue, string message)
        {
            var actualValue = _parameters.GetMaxValue(parameterType);
            Assert.AreEqual(expectedValue, actualValue, message);
        }

        [TestCase(ParameterType.BaseDiameter, Parameters.MinBaseDiameter,
            "Метод возвращает некорректное текущее значение параметра BaseDiameter.",
            TestName =
                "Тест метода GetValue: Получить текущее значение параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.ThroatDiameter, Parameters.MinThroatDiameter,
            "Метод возвращает некорректное текущее значение параметра ThroatDiameter.",
            TestName =
                "Тест метода GetValue: Получить текущее значение параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, Parameters.MinCarafeHeight,
            "Метод возвращает некорректное текущее значение параметра CarafeHeight.",
            TestName =
                "Тест метода GetValue: Получить текущее значение параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.HandleAngle, Parameters.MinHandleAngle,
            "Метод возвращает некорректное текущее значение параметра HandleAngle.",
            TestName =
                "Тест метода GetValue: Получить текущее значение параметра 'HandleAngle'.")]
        [TestCase(ParameterType.HandleLength, Parameters.MinHandleLength,
            "Метод возвращает некорректное текущее значение параметра HandleLength.",
            TestName =
                "Тест метода GetValue: Получить текущее значение параметра 'HandleLength'.")]
        [TestCase(ParameterType.StopperHeight, Parameters.MinStopperHeight,
            "Метод возвращает некорректное текущее значение параметра StopperHeight.",
            TestName =
                "Тест метода GetValue: Получить текущее значение параметра 'StopperHeight'.")]
        public void Test_GetValue_CorrectValue(ParameterType parameterType,
            double expectedValue, string message)
        {
            var actualValue = _parameters.GetValue(parameterType);
            Assert.AreEqual(expectedValue, actualValue, message);
        }

        [TestCase(ParameterType.BaseDiameter, 75,
            "Метод задает некорректное значение параметра 'BaseDiameter'.",
            TestName =
                "Позитивный тест метода SetValue: Задать значение для параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, 200,
            "Метод задает некорректное значение параметра 'CarafeHeight'.",
            TestName =
                "Позитивный тест метода SetValue: Задать значение для параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.ThroatDiameter, 30,
            "Метод задает некорректное значение параметра 'ThroatDiameter'.",
            TestName =
                "Позитивный тест метода SetValue: Задать значение для параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.StopperHeight, 35,
            "Метод задает некорректное значение параметра 'StopperHeight'.",
            TestName =
                "Позитивный тест метода SetValue: Задать значение для параметра 'StopperHeight'.")]
        [TestCase(ParameterType.HandleLength, 30,
            "Метод задает некорректное значение параметра 'HandleLength'.",
            TestName =
                "Позитивный тест метода SetValue: Задать значение для параметра 'HandleLength'.")]
        [TestCase(ParameterType.HandleAngle, 45,
            "Метод задает некорректное значение параметра 'HandleAngle'.",
            TestName =
                "Позитивный тест метода SetValue: Задать значение для параметра 'HandleAngle'.")]
        public void Test_SetValue_CorrectValue(ParameterType parameterType,
            double expectedValue, string message)
        {
            _parameters.SetValue(parameterType, expectedValue);
            var actualValue = _parameters.GetValue(parameterType);
            Assert.AreEqual(expectedValue, actualValue, message);
        }

         //TODO: RSDN
        [TestCase(ParameterType.BaseDiameter, -10,
            "Значение меньше минимально допустимого для параметра 'BaseDiameter'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение меньше минимально" +
            " допустимого значений параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, -10,
            "Значение меньше минимально допустимого для параметра 'CarafeHeight'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение меньше минимально" +
            " допустимого значений параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.ThroatDiameter, -10,
            "Значение меньше минимально допустимого для параметра 'ThroatDiameter'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение меньше минимально " +
            "допустимого значений параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.HandleAngle, -10,
            "Значение меньше минимально допустимого для параметра 'HandleAngle'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение меньше минимально" +
            " допустимого значений параметра 'HandleAngle'.")]
        [TestCase(ParameterType.HandleLength, -10,
            "Значение меньше минимально допустимого для параметра 'HandleLength'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение меньше минимально " +
            "допустимого значений параметра 'HandleLength'.")]
        [TestCase(ParameterType.StopperHeight, -10,
            "Значение меньше минимально допустимого для параметра 'StopperHeight'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение меньше минимально " +
            "допустимого значений параметра 'StopperHeight'.")]
        [TestCase(ParameterType.BaseDiameter, 1000,
            "Значение больше максимально допустимого для параметра 'BaseDiameter'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение больше максимально" +
            " допустимого значений параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, 1000,
            "Значение больше максимально допустимого для параметра 'CarafeHeight'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение больше максимально" +
            " допустимого значений параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.ThroatDiameter, 1000,
            "Значение больше максимально допустимого для параметра 'ThroatDiameter'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение больше максимально" +
            " допустимого значений параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.HandleAngle, 1000,
            "Значение больше максимально допустимого для параметра 'HandleAngle'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение больше максимально" +
            " допустимого значений параметра 'HandleAngle'.")]
        [TestCase(ParameterType.HandleLength, 1000,
            "Значение больше максимально допустимого для параметра 'HandleLength'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение больше максимально " +
            "допустимого значений параметра 'HandleLength'.")]
        [TestCase(ParameterType.StopperHeight, 1000,
            "Значение больше максимально допустимого для параметра 'StopperHeight'.",
            TestName =
                "Негативный тест метода SetValue: Попытка задать значение больше максимально " +
            "допустимого значений параметра 'StopperHeight'.")]
        public void Test_SetValue_WrongValue(ParameterType parameterType,
            double wrongValue,
            string message)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { _parameters.SetValue(parameterType, wrongValue); }, message);
        }


        [TestCase(ParameterType.BaseDiameter, 75,
            "Значение не входит в допустимый диапазон значений для параметра 'BaseDiameter'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод допустимного значения для параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, 200,
            "Значение не входит в допустимый диапазон значений для параметра 'CarafeHeight'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод допустимного значения для параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.ThroatDiameter, 30,
            "Значение не входит в допустимый диапазон значений для параметра 'ThroatDiameter'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод допустимного значения для параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.StopperHeight, 35,
            "Значение не входит в допустимый диапазон значений для параметра 'StopperHeight'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод допустимного значения для параметра 'StopperHeight'.")]
        [TestCase(ParameterType.HandleAngle, 45,
            "Значение не входит в допустимый диапазон значений для параметра 'HandleAngle'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод допустимного значения для параметра 'HandleAngle'.")]
        [TestCase(ParameterType.HandleLength, 30,
            "Значение не входит в допустимый диапазон значений для параметра 'HandleLength'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод допустимного значения для параметра 'HandleLength'.")]
        public void Test_IsCorrectValue_CorrectValue(ParameterType parameterType,
            double value, string message)
        {
            Assert.IsTrue(_parameters.IsCorrectValue(parameterType, value), message);
        }

        [TestCase(ParameterType.BaseDiameter, -100,
            "Значение входит в допустимый диапазон значений для параметра 'BaseDiameter'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод значения не входящий в допустимный диапазон" +
            " для параметра 'BaseDiameter'.")]
        [TestCase(ParameterType.CarafeHeight, -100,
            "Значение входит в допустимый диапазон значений для параметра 'CarafeHeight'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод значения не входящий в допустимный диапазон " +
            "для параметра 'CarafeHeight'.")]
        [TestCase(ParameterType.ThroatDiameter, -100,
            "Значение входит в допустимый диапазон значений для параметра 'ThroatDiameter'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод значения не входящий в допустимный диапазон" +
            " для параметра 'ThroatDiameter'.")]
        [TestCase(ParameterType.StopperHeight, -100,
            "Значение входит в допустимый диапазон значений для параметра 'StopperHeight'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод значения не входящий в допустимный диапазон" +
            " для параметра 'StopperHeight'.")]
        [TestCase(ParameterType.HandleAngle, -100,
            "Значение входит в допустимый диапазон значений для параметра 'HandleAngle'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод значения не входящий в допустимный диапазон" +
            " для параметра 'HandleAngle'.")]
        [TestCase(ParameterType.HandleLength, -100,
            "Значение входит в допустимый диапазон значений для параметра 'HandleLength'.",
            TestName =
                "Тест метода IsCorrectValue: Ввод значения не входящий в допустимный диапазон " +
            "для параметра 'HandleLength'.")]
        public void Test_IsCorrectValue_WrongValue(ParameterType parameterType,
            double value, string message)
        {
            Assert.IsFalse(_parameters.IsCorrectValue(parameterType, value), message);
        }
        
    }
}