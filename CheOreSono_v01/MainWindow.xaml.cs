using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

/* Autore Paolo Maria Guardiani
 * 
 * DEO GRATIAS!
 * 
 * Ho trovato le immagini su clipart-library.com e su burst.shopify.com
 * Ho composto l'introduzione al gioco con MuseScore
 * Ho trovato i suoni su freesound.org
 */

namespace CheOreSono_v01
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int counterMinuti = 0;
        public int counterOre = 0;
        Random randomNumber = new Random();
        int oreCasuali;
        int minutiCasuali;
        int oreCalcolate;
        int minutiCalcolati;
        int punteggio;

        private SoundPlayer soundIntro, soundClick, soundNew, soundCoin, soundError;
        
        public MainWindow()
        {
            InitializeComponent();
            controlla.IsEnabled = false;
            soundIntro = new SoundPlayer(Properties.Resources.intro_che_ore_sono_3a);
            soundClick = new SoundPlayer(Properties.Resources.button_click);
            soundNew = new SoundPlayer(Properties.Resources.elevator_bell);
            soundCoin = new SoundPlayer(Properties.Resources.coin);
            soundError = new SoundPlayer(Properties.Resources.sound_error_soft);


            soundIntro.Play();
        }



        private void hour1_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            // Sposto la lancetta delle ore di una posizione in avanti
            counterOre += 30;
            RotateTransform rotate_ore = new RotateTransform(counterOre, 10, 100);
            lancettaOre.RenderTransform = rotate_ore;
            trasformaOreEMinuti(ref counterOre, ref counterMinuti);
        }

        private void minute1_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            // Thanks to: https://youtu.be/-BbsZErVBqI
            // Sposto la lancetta di un minuto in avanti spostandola di 6 gradi
            counterMinuti += 6;
            RotateTransform rotate_minuti = new RotateTransform(counterMinuti, 10, 100);
            lancettaMinuti.RenderTransform = rotate_minuti;
            trasformaOreEMinuti(ref counterOre, ref counterMinuti);
        }

        private void minute5_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            // Sposto la lancetta di cinque minuti in avanti spostandola di 30 gradi
            counterMinuti += 30;
            RotateTransform rotate_minuti = new RotateTransform(counterMinuti, 10, 100);
            lancettaMinuti.RenderTransform = rotate_minuti;
            trasformaOreEMinuti(ref counterOre, ref counterMinuti);
        }

        private void hour_1_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            // Sposto la lancetta delle ore di una posizione indietro
            counterOre -= 30;
            RotateTransform rotate_ore = new RotateTransform(counterOre, 10, 100);
            lancettaOre.RenderTransform = rotate_ore;
            trasformaOreEMinuti(ref counterOre, ref counterMinuti);
        }

        private void minute_1_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            // Sposto la lancetta di un minuto indietro spostandola di 6 gradi
            counterMinuti -= 6;
            RotateTransform rotate_minuti = new RotateTransform(counterMinuti, 10, 100);
            lancettaMinuti.RenderTransform = rotate_minuti;
            trasformaOreEMinuti(ref counterOre, ref counterMinuti);
        }

        private void minute_5_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            // Sposto la lancetta di cinque minuti indietro spostandola di 30 gradi
            counterMinuti -= 30;
            RotateTransform rotate_minuti = new RotateTransform(counterMinuti, 10, 100);
            lancettaMinuti.RenderTransform = rotate_minuti;
            trasformaOreEMinuti(ref counterOre, ref counterMinuti);
        }

        private void nuovoGioco_Click(object sender, RoutedEventArgs e)
        {
            soundNew.Play();
            // Visualizzo le ore e i minuti casuali per il gioco
            minutiCasuali = randomNumber.Next(0, 59);
            oreCasuali = randomNumber.Next(0, 23);
            string ore = oreCasuali.ToString();
            labelOre.Content = (oreCasuali < 10) ? $"0{ore}" : ore;
            string minuti = minutiCasuali.ToString();
            labelMinuti.Content = (minutiCasuali < 10) ? $"0{minuti}" : minuti;
            controlla.IsEnabled = true;
            // Scrivo
            oraImpostata.Content = $"Sono le ore --:-- oppure le ore --:--";
        }

        private void controlla_Click(object sender, RoutedEventArgs e)
        {
            soundClick.Play();
            if ((oreCasuali == oreCalcolate || oreCasuali - 12 == oreCalcolate) && (minutiCasuali == minutiCalcolati || minutiCasuali - 60 == minutiCalcolati))
            {
                soundCoin.Play();
                punteggio++;
                lblPunteggio.Content = $"PUNTEGGIO: {punteggio}";
                controlla.IsEnabled = false;
            }
            else
            {
                soundError.Play();
                punteggio--;
                lblPunteggio.Content = $"PUNTEGGIO: {punteggio}";
            }

        }

        private void trasformaOreEMinuti(ref int counterOre, ref int counterMinuti)
        {
            oreCalcolate = counterOre / 30;
            if (oreCalcolate >= 12)
            {
                oreCalcolate %= 12;
            }
            else if (oreCalcolate <= 0)
            {
                oreCalcolate += 12;
            }
            minutiCalcolati = counterMinuti / 6;
            if (minutiCalcolati >= 60)
            {
                minutiCalcolati %= 60;
            }
            else if (minutiCalcolati <= 0)
            {
                minutiCalcolati += 60;
            }
            string oreAm, orePm, minuti;
            oreAm = (oreCalcolate < 10) ? $"0{oreCalcolate.ToString()}" : oreCalcolate.ToString();
            orePm = (oreCalcolate + 12).ToString();
            minuti = (minutiCalcolati < 10) ? $"0{minutiCalcolati.ToString()}" : minutiCalcolati.ToString();
            oraImpostata.Content = $"Sono le ore {oreAm}:{minuti} oppure le ore {orePm}:{minuti}";
        }
    }
}
