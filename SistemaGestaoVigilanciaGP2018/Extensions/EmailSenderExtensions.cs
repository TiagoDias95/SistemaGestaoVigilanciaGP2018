using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SistemaGestaoVigilanciaGP2018.Services;

namespace SistemaGestaoVigilanciaGP2018.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirme o seu email",
                $"Por favor confirme a sua conta ao clicar no seguinte link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
