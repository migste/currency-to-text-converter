using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyToTextConverter.GUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _amount = string.Empty;
        private string _selectedLanguage = "en";
        private string _result = string.Empty;
        private string _errorMessage = string.Empty;

        public string Amount { get => _amount; set { _amount = value; OnPropertyChanged(nameof(Amount)); } }
        public string SelectedLanguage { get => _selectedLanguage; set { _selectedLanguage = value; OnPropertyChanged(nameof(SelectedLanguage)); } }
        public string Result { get => _result; set { _result = value; OnPropertyChanged(nameof(Result)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ICommand ConvertCommand { get; }

        private readonly Services.ConversionService _conversionService = new Services.ConversionService();

        public MainViewModel()
        {
            ConvertCommand = new AsyncCommand(ConvertAsync);
        }

        private async Task ConvertAsync()
        {
            ErrorMessage = string.Empty;
            Result = string.Empty;

            if (string.IsNullOrWhiteSpace(Amount))
            {
                ErrorMessage = "Please enter an amount";
                return;
            }

            try
            {
                var (Success, Text, ErrorMessage) = await _conversionService.ConvertAsync(Amount, SelectedLanguage);
                if (!Success)
                {
                    this.ErrorMessage = ErrorMessage ?? "Unknown error";
                    return;
                }

                Result = Text ?? string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error contacting server: " + ex.Message;
            }
        }


    }
}
