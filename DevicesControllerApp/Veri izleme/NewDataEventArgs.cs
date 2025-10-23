using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesControllerApp.Veri_izleme
{
    // Cihazdan gelen tüm sensör verilerini tek bir pakette taşır
    internal class NewDataEventArgs : EventArgs
    {
        public List<double> SagTabanData { get; set; }  // Sağ taban sensörleri
        public List<double> SolTabanData { get; set; }  // Sol taban sensörleri
        public List<double> SagTopukData { get; set; }  // Sağ topuk sensörleri
        public List<double> SolTopukData { get; set; }  // Sol topuk sensörleri
        public double AgirlikDengesi { get; set; }      // Ağırlık dengesi verisi
        public bool IsStepDetected { get; set; }        // Adım algılandı mı? (true/false)
    }
}