using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_integradora.clases
{
    public abstract class MensajeBase
    {
        public string Usuario {  get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }

        public abstract string Formatear();
    }


    public class MensajeQueja : MensajeBase
    {
        public override string Formatear()
        {
            return $"[QUEJA] {Fecha} - {Usuario}: {Contenido}";
        }
    }

    public class MensajeSugerencia : MensajeBase
    {
        public override string Formatear()
        {
            return $"[SUGERENCIA] {Fecha} - {Usuario}: {Contenido}";
        }
    }

    public class MensajeGeneral : MensajeBase
    {
        public override string Formatear()
        {
            return $"[GENERAL] {Fecha} - {Usuario}: {Contenido}";
        }
    }




}
