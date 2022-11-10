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


        //ContadorInterno y contador visual, y estado de preventivas
        int t = 0, tV = 1;
        bool cambio = true, blnPreventivas = false;


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (btnIniciar.BackColor == Color.FromArgb(38, 35, 53))
            {
                tmrTiempos.Start();
                btnIniciar.BackColor = Color.FromArgb(0, 180, 0);
                btnIniciar.Text = "Pausar";
            }
            else
            {
                tmrTiempos.Stop();
                btnIniciar.BackColor = Color.FromArgb(38, 35, 53);
                btnIniciar.Text = "Iniciar";
            }
        }

        //Timer que mueve los tiempos
        private void tmrTiempos_Tick(object sender, EventArgs e)
        {
            if (!blnPreventivas)
                CambiarLuces();
            else
                ActivarPreventivas();
        }

        //botón para hacer pruebas
        private void btnCambioManual_Click(object sender, EventArgs e)
        {
            if (!blnPreventivas)
                CambiarLuces();
            else
                ActivarPreventivas();
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            t = 0;
            cambio = true;
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

        private void CambiarLuces()
        {
            if (cambio) //true es verticales
            {
                //Inicio en verde frontal
                if (t <= 28)
                {
                    ptbVerdeA.Visible = true;
                    ptbVerdeB.Visible = true;
                    ptbRojoC.Visible = true;
                    ptbRojoD.Visible = true;
                    ptbAmarilloA.Visible = false;
                    ptbAmarilloB.Visible = false;
                    ptbAmarilloC.Visible = false;
                    ptbAmarilloD.Visible = false;
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
                    ptbRojoA.Visible = true;
                    ptbRojoB.Visible = true;
                    ptbAmarilloA.Visible = false;
                    ptbAmarilloB.Visible = false;
                    ptbAmarilloC.Visible = false;
                    ptbAmarilloD.Visible = false;
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

        private void btnPreventivas_Click(object sender, EventArgs e)
        {
            //Estado inicial de preventivas
            t = 0;
            tV = 0;
            ptbVerdeA.Visible = false;
            ptbVerdeB.Visible = false;
            ptbVerdeC.Visible = false;
            ptbVerdeD.Visible = false;
            ptbAmarilloA.Visible = true;
            ptbAmarilloB.Visible = true;
            ptbAmarilloC.Visible = true;
            ptbAmarilloD.Visible = true;
            ptbRojoA.Visible = false;
            ptbRojoB.Visible = false;
            ptbRojoC.Visible = false;
            ptbRojoD.Visible = false;
            lblTiempo.ForeColor = Color.FromArgb(255, 255, 0);

            //Activar las preventivas
            ActivarPreventivas();
            blnPreventivas = !blnPreventivas;
            if (blnPreventivas)
                btnPreventivas.BackColor = Color.FromArgb(138, 138, 0);
            else
                btnPreventivas.BackColor = Color.FromArgb(38, 35, 53);
        }

        private void ImprimirTiempo()
        {
            if (!blnPreventivas)
                lblTiempo.Text = tV.ToString();
            else
                lblTiempo.Text = "0";
        }

        private void ActivarPreventivas()
        {
            if (ptbAmarilloA.Visible == true)
            {
                ptbAmarilloA.Visible = false;
                ptbAmarilloB.Visible = false;
                ptbAmarilloC.Visible = false;
                ptbAmarilloD.Visible = false;
                lblTiempo.ForeColor = Color.FromArgb(90,90,90);
            }
            else
            {
                ptbAmarilloA.Visible = true;
                ptbAmarilloB.Visible = true;
                ptbAmarilloC.Visible = true;
                ptbAmarilloD.Visible = true;
                lblTiempo.ForeColor = Color.FromArgb(255, 255, 0);
            }
            ptbRojoA.Visible = false;
            ptbRojoB.Visible = false;
            ptbRojoC.Visible = false;
            ptbRojoD.Visible = false;
            ptbVerdeA.Visible = false;
            ptbVerdeB.Visible = false;
            ptbVerdeC.Visible = false;
            ptbVerdeD.Visible = false;
            ImprimirTiempo();
        }
    }
}
