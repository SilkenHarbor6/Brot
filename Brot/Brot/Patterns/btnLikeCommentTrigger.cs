﻿

namespace Brot.Patterns
{
    using Services;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;
    public class btnLikeCommentTrigger : TriggerAction<ImageButton>
    {
        protected async override void Invoke(ImageButton sender)
        {
            bool isliked;
            isliked = (bool)((ImageButton)sender).CommandParameter;
            //Saber si ya era like o no? Retorna FALSE para el pin negro y retorna True para el pin Naranja
            var like = new like_comentarioModel()
            {
                id_comentario = Convert.ToInt32(((ImageButton)sender).ClassId),
                id_user = Singleton.Instance.User.id_user
            };
            if (isliked)
            {
                //Se quita el like
                ((ImageButton)sender).Source = DLL.constantes.dislikeImage;
                await RestClient.Post<like_comentarioModel>("like_comentario/borrar", like);
            }
            else
            {
                ((ImageButton)sender).Source = DLL.constantes.likeImage;
                await RestClient.Post<like_comentarioModel>("like_comentario", like);
            }
            ((ImageButton)sender).CommandParameter = !isliked;
        }
    }
}
