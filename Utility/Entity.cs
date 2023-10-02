using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    // Le classi devono per forza essere public o non saranno utilizzabili come pacchetto
    public class Entity
    {
        public int Id { get; set; }

        public Entity() { }

        public Entity(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            string ris = "";

            foreach (PropertyInfo proprieta in this.GetType().GetProperties())
                ris += $"{proprieta.Name}: {proprieta.GetValue(this)}\n";

            return ris;
        }

        public void FromDictionary(Dictionary<string, string> riga)
        {
            foreach (PropertyInfo proprieta in this.GetType().GetProperties())
            {
                if (riga.ContainsKey(proprieta.Name.ToLower()))
                {
                    object valore = riga[proprieta.Name.ToLower()];

                    switch (proprieta.PropertyType.Name.ToLower())
                    {
                        case "int32":
                            valore = int.Parse(riga[proprieta.Name.ToLower()]);
                            break;
                        case "double":
                            valore = double.Parse(riga[proprieta.Name.ToLower()]);
                            break;
                        case "boolean":
                            valore = bool.Parse(riga[proprieta.Name.ToLower()]);
                            break;
                        case "datetime":
                            valore = DateTime.Parse(riga[proprieta.Name.ToLower()]);
                            break;
                    }

                    proprieta.SetValue(this, valore);
                }
            }
        }
    }
}
