﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace I9Solucoes
{
    public class RespostaViewModel
    {
        [AllowHtml]
        public string Resposta { get; set; }
    }
}