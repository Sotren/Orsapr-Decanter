using NUnit.Framework;

namespace CarafeModule.UnitTests
{
    /// <summary>
    /// Тестирование класса Parameter.
    /// </summary>
    [TestFixture(Description = "Модульные тесты класса Parameter.")]
    public class ParameterTest
    {
        private const double _minValue = 10;
        private const double _maxValue = 99;
        private const double _value = 30;


        private Parameter _parameter;

        [SetUp]
        public void CreateTestParameter()
        {
            _parameter = new Parameter(_minValue, _maxValue, _value);
        }

        [TestCase(_minValue, "Сеттер некорректно записал данные.",
            TestName =
                "Позитивный тест сеттера свойства MinValue: Задать значение при создании объекта.")]
        public void Test_MinValue_Set_CorrectValue(double expectedValue, string message)
        {
            var parameter = new Parameter(expectedValue, _maxValue, _value);
            Assert.AreEqual(expectedValue, parameter.MinValue, message);
        }

        [TestCase(_minValue, "Геттер некорректно возвращает значение.",
            TestName = "Позитивный тест геттера свойства MinValue.")]
        public void Test_MinValue_Get_CorrectValue(double expectedMin, string message)
        {
            var parameter = new Parameter(expectedMin, _maxValue, _value);
            var actual = parameter.MinValue;
            Assert.AreEqual(expectedMin, actual, message);
        }

        [TestCase(_maxValue, _maxValue, "Сеттер некорректно записал данные.", TestName =
            "Позитивный тест сеттера свойства MaxValue: Задать максимальное значение объекта.")]
        [TestCase(-10, _minValue, "Сеттер записал значение меньше допустимого.",
            TestName =
                "Позитивный тест сеттера свойства MaxValue: Попытка присвоить значение меньше минимально допустимого.")]
        public void Test_MaxValue_Set_CorrectValue(double value, double expectedValue,
            string message)
        {
            _parameter.MaxValue = value;
            Assert.AreEqual(expectedValue, _parameter.MaxValue, message);
        }

        [TestCase(100, "Геттер возвращает некорректное значение.",
            TestName = "Позитивный тест геттера свойства MaxValue.")]
        public void Test_MaxValue_Get_CorrectValue(double expectedValue, string message)
        {
            _parameter.MaxValue = expectedValue;
            var actual = _parameter.MaxValue;
            Assert.AreEqual(expectedValue, actual, message);
        }

        [TestCase(_value, _value, "Сеттер некорректно записал данные.",
            TestName = "Позитивный тест сеттера свойства Value: Задать новое значение.")]
        [TestCase(1000, _maxValue, "Сеттер некорректно записал данные.",
            TestName =
                "Позитивный тест сеттера свойства Value: Задать значение, превышающее максимально допустимое.")]
        [TestCase(-10, _minValue, "Сеттер некорректно записал данные.",
            TestName =
                "Позитивный тест сеттера свойства Value: Задать значение, меньше минимально допустимое.")]
        public void Test_Value_Set_CorrectValue(double value, double expectedValue,
            string message)
        {
            _parameter.Value = value;
            Assert.AreEqual(expectedValue, _parameter.Value, message);
        }

        [TestCase(_value, "Геттер возвращает некорректное значение.",
            TestName = "Позитивный тест геттера свойства Value.")]
        public void Test_Value_Get_CorrectValue(double expectedValue, string message)
        {
            _parameter.Value = expectedValue;
            var actual = _parameter.Value;
            Assert.AreEqual(expectedValue, actual, message);
        }

        //TODO: Duplication
        [TestCase("Произошла ошибка при сравнении объектов.",
            TestName = "Позитивный тест метода Equals.")]
        public void Test_EqualsParameter_CorrectValue(string message)
        {
            var actualParameter = new Parameter(_minValue, _maxValue, _value);
            Assert.IsTrue(actualParameter.Equals(_parameter), message);
        }

        [TestCase(_maxValue, _minValue, _maxValue, "Ошибка при сравнении.",
            TestName = "Негативный тест метода Equals.")]
        public void Test_EqualsParameter_WrongValue(double minValue, double maxValue,
            double value, string message)
        {
            var actualParameter = new Parameter(minValue, maxValue, value);
            Assert.IsFalse(actualParameter.Equals(_parameter), message);
        }

        //TODO: Duplication
        [TestCase("Некорректное создание конструктора.",
            TestName = "Тестирование конструктора класса.")]
        public void Test_Parameter(string message)
        {
            var actualParameter = new Parameter(_minValue, _maxValue, _value);
            Assert.IsTrue(actualParameter.Equals(_parameter), message);
        }
    }
}