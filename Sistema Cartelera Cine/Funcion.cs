namespace Sistema_Cartelera_Cine
{
    public class Funcion
    {
        public int Id { get; set; }
        public string Pelicula { get; set; }
        public string Sala { get; set; }
        public decimal Precio { get; set; }

        public Funcion()
        {
            Pelicula = "";
            Sala = "";
        }

        public override string ToString()
        {
            return $"{Id}. {Pelicula} ({Sala}) - Q {Precio:F2}";
        }
    }
}