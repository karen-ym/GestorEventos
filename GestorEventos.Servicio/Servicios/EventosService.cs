﻿using GestorEventos.Servicios.Entidades;
using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public class EventoService
    {
        public IEnumerable<Evento> Eventos {  get; set; }

        public EventoService(){
            Eventos = new List<Evento> {
                new Evento { IdEvento = 1, CantidadPersonas = 5, FechaEvento = DateTime.Now, IdPersonaAgasajada = 1, IdPersonaContacto = 2, IdTipoDespedida = 1, NombreEvento = "Despedida de Soltero para Diego", Visible = true},
                new Evento { IdEvento = 2, CantidadPersonas = 10, FechaEvento = DateTime.Now, IdPersonaAgasajada = 3, IdPersonaContacto = 4, IdTipoDespedida = 2, NombreEvento = "Despedida de Soltero para Diego",  Visible = true}
            };
        }

        public IEnumerable<Evento> GetAllEventos() {
            return Eventos.Where(x => x.Visible == true);
        }

        public Evento GetEventoPorId(int IdEvento) {
            var eventos = this.Eventos.Where(x => x.IdEvento == IdEvento);

            if (eventos == null) {
                return null;
            }

            return eventos.First();
        }

        public bool PostNuevoEvento(Evento evento)
        {
            try
            {
                List<Evento> lista = this.Eventos.ToList();
                lista.Add(evento);
                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PutNuevoEvento(int idEvento, Evento evento)
        {
            try
            {
                var eventoDeLista = this.Eventos.Where(x => x.IdEvento == idEvento).First();
                
                eventoDeLista.NombreEvento = evento.NombreEvento;
                eventoDeLista.FechaEvento = evento.FechaEvento;
                eventoDeLista.CantidadPersonas = evento.CantidadPersonas;
                eventoDeLista.IdPersonaContacto = evento.IdPersonaContacto;
                eventoDeLista.IdPersonaAgasajada = evento.IdPersonaAgasajada;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEvento(int idEvento) {
            try
            {
                var eventoAEliminar = this.Eventos.Where(x => x.IdEvento == idEvento).First();
                var listaEventos = this.Eventos.ToList();
                eventoAEliminar.Visible = false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void PostNuevoEventoCompleto(EventoModel eventoModel)
        {
            PersonaService personaService = new PersonaService();
            int idPersonaAgasajada = personaService.AgregarNuevaPersona(eventoModel.PersonaAgasajada);
            int idPersonaContacto = personaService.AgregarNuevaPersona(eventoModel.PersonaContacto);

            eventoModel.evento.IdPersonaAgasajada = idPersonaAgasajada;
            eventoModel.evento.IdPersonaContacto = idPersonaContacto;
            eventoModel.evento.Visible = true;

            this.PostNuevoEvento(eventoModel.evento);

            foreach (Servicio servicio in eventoModel.ListaDeServiciosContratados) {
                ServicioService servicioService = new ServicioService();
                servicioService.AgregarNuevoServicio(servicio);
            }

        }
    };
}
