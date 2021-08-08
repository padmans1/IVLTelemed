using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Telemed.Patient;

namespace BlazorHero.CleanArchitecture.Client.Pages.Telemed
{
    public partial class AddEditPatientModal
    {
        [Inject] private IPatientManager PatientManager { get; set; }

        [Parameter] public AddEditPatientCommand AddEditPatientModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await PatientManager.SaveAsync(AddEditPatientModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

       
        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            //await LoadImageAsync();
            //await LoadBrandsAsync();
        }


        //private async Task LoadImageAsync()
        //{
        //    var data = await ProductManager.GetProductImageAsync(AddEditPatientModel.Id);
        //    if (data.Succeeded)
        //    {
        //        var imageData = data.Data;
        //        if (!string.IsNullOrEmpty(imageData))
        //        {
        //            AddEditPatientModel.ImageDataURL = imageData;
        //        }
        //    }
        //}

        private void DeleteAsync()
        {
            //AddEditPatientModel.ImageDataURL = null;
            AddEditPatientModel.UploadRequest = new UploadRequest();
        }

        private IBrowserFile _file;

        //private async Task UploadFiles(InputFileChangeEventArgs e)
        //{
        //    _file = e.File;
        //    if (_file != null)
        //    {
        //        var extension = Path.GetExtension(_file.Name);
        //        var format = "image/png";
        //        var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
        //        var buffer = new byte[imageFile.Size];
        //        await imageFile.OpenReadStream().ReadAsync(buffer);
        //        AddEditPatientModel.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
        //        AddEditPatientModel.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.Product, Extension = extension };
        //    }
        //}

        //private async Task<IEnumerable<int>> SearchBrands(string value)
        //{
        //    // In real life use an asynchronous function for fetching data from an api.
        //    await Task.Delay(5);

        //    // if text is null or empty, show complete list
        //    if (string.IsNullOrEmpty(value))
        //        return _brands.Select(x => x.Id);

        //    return _brands.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
        //        .Select(x => x.Id);
        //}
    }
}