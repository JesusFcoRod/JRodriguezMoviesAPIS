﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCines { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Venta { get; set; }

    public int? Zona { get; set; }

    public virtual Zona? ZonaNavigation { get; set; }

    public string NombreZona { get; set; }
}
