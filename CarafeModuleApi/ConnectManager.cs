using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using CarafeModuleUI;

namespace CarafeModuleApi
{
    /// <summary>
    /// Класс, реализующий подключение между AutoCAD и плагином.
    /// </summary>
    public class ConnectManager : IExtensionApplication
    {
        [CommandMethod("BuildCarafe")]
        public void BuildCarafe()
        {
            var carafeForm = new CarafeForm();
            var dialogResult = carafeForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var parameters = carafeForm.Parameters;
                var modelBuilder = new ModelBuilder(parameters);
                modelBuilder.DrawCarafe();
            }
        }

        /// <summary>
        ///  Метод, который срабатывает при инициализации плагина.
        /// </summary>
        public void Initialize()
        {
            MessageBox.Show("Плагин загружен. Введите команду 'BuildCarafe' для работы с плагином", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Метод срабатывает при выгрузке плагина.
        /// </summary>
        public void Terminate()
        {
        }
    }
}