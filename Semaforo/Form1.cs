using System;
using System.Drawing;
using System.Windows.Forms;

namespace Semaforo
{
    public partial class FrmSemaforo : Form
    {
        public FrmSemaforo()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            //t = 1;
            tmrTiempos.Start();
        }

        private void tmrTiempos_Tick(object sender, EventArgs e)
        {
            CambiarLuces();
        }

        private void btnCambioManual_Click(object sender, EventArgs e)
        {
            CambiarLuces();
        }

        private void btnPausa_Click(object sender, EventArgs e)
        {
            tmrTiempos.Stop();
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            t = 0;
            tV = 1;
            ptbVerdeA.Visible = true;
            ptbVerdeB.Visible = true;
            ptbVerdeC.Visible = false;
            ptbVerdeD.Visible = false;
            ptbAmarilloA.Visible = false;
            ptbAmarilloB.Visible = false;
            ptbAmarilloC.Visible = false;
            ptbAmarilloD.Visible = false;
            ptbRojoA.Visible = false;
            ptbRojoB.Visible = false;
            ptbRojoC.Visible = true;
            ptbRojoD.Visible = true;
            lblTiempo.ForeColor = Color.FromArgb(0, 255, 0);
            ImprimirTiempo();
        }

        //ContadorInterno y contador visual
        int t = 0, tV = 1;
        bool cambio = true;

        private void CambiarLuces()
        {
            if (cambio) //true es verticales
            {
                //Inicio en verde frontal
                if (t <= 28)
                {
                    ptbVerdeA.Visible = true;
                    ptbVerdeB.Visible = true;
                }
                else
                {
                    //parpadeo de verde
                    if (t >= 29 && t <= 36)
                    {
                        ptbVerdeA.Visible = false;
                        ptbVerdeB.Visible = false;
                        if (t % 2 == 0)
                        {
                            ptbVerdeA.Visible = false;
                            ptbVerdeB.Visible = false;
                        }
                        else
                        {
                            ptbVerdeA.Visible = true;
                            ptbVerdeB.Visible = true;
                        }
                    }
                    else
                    {
                        //encendido de amarillo 2.5 segundos = 5 tiempos
                        if (t >= 37 && t <= 41)
                        {
                            ptbAmarilloA.Visible = true;
                            ptbAmarilloB.Visible = true;
                        }
                        else
                        {
                            ptbAmarilloA.Visible = false;
                            ptbAmarilloB.Visible = false;
                            //apagar amarillo y encender rojo
                            if (t >= 43 && t <= 46)
                            {
                                ptbRojoA.Visible = true;
                                ptbRojoB.Visible = true;
                            }
                            else
                            {
                                if (t == 47)
                                {
                                    cambio = false;
                                    //t = 0;
                                    ptbRojoC.Visible = false;
                                    ptbRojoD.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            else //falso es horizontales
            {
                //Inicio en verde horizontal
                if (t <= 28)
                {
                    ptbVerdeC.Visible = true;
                    ptbVerdeD.Visible = true;
                }
                else
                {
                    //parpadeo de verde
                    if (t >= 29 && t <= 36)
                    {
                        ptbVerdeC.Visible = false;
                        ptbVerdeD.Visible = false;
                        if (t % 2 == 0)
                        {
                            ptbVerdeC.Visible = false;
                            ptbVerdeD.Visible = false;
                        }
                        else
                        {
                            ptbVerdeC.Visible = true;
                            ptbVerdeD.Visible = true;
                        }
                    }
                    else
                    {
                        //encendido de amarillo 2.5 segundos = 5 tiempos
                        if (t >= 37 && t <= 41)
                        {
                            ptbAmarilloC.Visible = true;
                            ptbAmarilloD.Visible = true;
                        }
                        else
                        {
                            ptbAmarilloC.Visible = false;
                            ptbAmarilloD.Visible = false;
                            //apagar amarillo y encender rojo
                            if (t >= 43 && t <= 46)
                            {
                                ptbRojoC.Visible = true;
                                ptbRojoD.Visible = true;
                            }
                            else
                            {
                                if (t == 47)
                                {
                                    cambio = true;
                                    ptbRojoA.Visible = false;
                                    ptbRojoB.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            Contar();
            ImprimirTiempo();
        }

        private void Contar()
        {
            //verde encendido
            if (t >= 1 & t <= 30)
            {
                tV = t/2 + 1;
                lblTiempo.ForeColor = Color.FromArgb(0, 255, 0);
            }
            
            //verde parpadea
            if (t >= 30 & t <= 34)
            {
                tV = (t - 28)/2;
            }

            //amarillo
            if (t >= 36 & t <= 40)
            {
                tV = (t - 34)/2;
                lblTiempo.ForeColor = Color.FromArgb(255, 255, 0);
            }

            //rojo y reinicio
            if (t >= 42 & t <= 45)
            {
                tV = (t - 40)/2;
                lblTiempo.ForeColor = Color.FromArgb(255, 0, 0);
            }

            if (t == 47)
            {
                t = 0;
                tV = 1;
                lblTiempo.ForeColor = Color.FromArgb(0, 255, 0);
            }

            t++;
        }

        private void BtnCambiarColor_Click(object sender, EventArgs e)
        {
            lblTiempo.ForeColor = Color.FromArgb(0,0,0);
        }

        private void ImprimirTiempo()
        {
            lblTiempo.Text = tV.ToString();
            //lblTiempoInterno.Text = t.ToString();
        }
    }
}
