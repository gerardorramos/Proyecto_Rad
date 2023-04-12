using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LRAD012023.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAlum : ContentPage
    {
        // Variable global de la foto
        Plugin.Media.Abstractions.MediaFile filefoto = null;

        //Function que convierta imagen to base64
        private String ImagetoBase64()
        {
            if (filefoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = filefoto.GetStream();
                    stream.CopyTo(memory);
                    byte[] bytefoto = memory.ToArray();
                    string fotoBase64 = Convert.ToBase64String(bytefoto);
                    return fotoBase64;
                }
            }
            return null;
        }

        public PageAlum()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    //txtlatitud.Text = Convert.ToString(location.Latitude);
                    //txtlongitud.Text = Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception)
            {
            }

        }

        private async void btnmostrar_Clicked(object sender, EventArgs e)
        //validaciones
        
        {

            if (string.IsNullOrEmpty(txtnombre.Text))
            {
                await DisplayAlert("Aviso", "Debe de agregar un nombre", "ok");
                txtnombre.Focus();
            }

            else

         if (string.IsNullOrEmpty(txtapellidos.Text))
            {
                await DisplayAlert("Aviso", "Debe de agregar un Apellido", "ok");
                txtapellidos.Focus();
            }
            else

         if (string.IsNullOrEmpty(txttelefono.Text))
            {
                await DisplayAlert("Aviso", "Debe de agregar un Telefono", "ok");
                txttelefono.Focus();
            }
            else

         if (string.IsNullOrEmpty(txtedad.Text))
            {
                await DisplayAlert("Aviso", "Debe de agregar una Edad", "ok");
                txtedad.Focus();
            }
            else

         if (string.IsNullOrEmpty(txtnota.Text))
            {
                await DisplayAlert("Aviso", "Debe de agregar una Nota", "ok");
                txtnota.Focus();

            }
            else
            { /*guardar los datos y enviar a pagina resultado*/

                var alum = new Models.Contactos
                {
                    nombres = txtnombre.Text,
                    telefono = txttelefono.Text,
                    apellidos = txtapellidos.Text,
                    edad = txtedad.Text,
                    pais = cbpais.SelectedItem.ToString(),
                    nota = txtnota.Text,
                    foto = ImagetoBase64()
                };

                if (await App.DBAlum.SaveAlum(alum) > 0)
                    await DisplayAlert("Aviso", "Contacto Ingresado", "OK");
                else
                    await DisplayAlert("Aviso", "Error", "OK");

                var page = new Views.PageResultado();
                page.BindingContext = alum;
                await Navigation.PushAsync(page);

            }
        }

            private async void btnfoto_Clicked(object sender, EventArgs e)
            //aqui esta la foto
            {
                filefoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "MiApp",
                    Name = "foto.jpg"
                });

                if (filefoto != null)
                {
                    foto.Source = ImageSource.FromStream(() => { return filefoto.GetStream(); });
                }
            }
        }
    }
