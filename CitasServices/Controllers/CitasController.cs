using RabbitMQ.Client;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace CitasServices.Controllers
{
    public class CitasController : Controller
    {
        private CitasDbContext db = new CitasDbContext();

        // GET: Citas
        public ActionResult Index()
        {
            return View(db.Citas.ToList());
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Paciente,Medico,Especialidad,Estado")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cita);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Paciente,Medico,Especialidad,Estado")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();

                // Si la cita ha finalizado, enviamos el mensaje a RabbitMQ
                if (cita.Estado == "Finalizada")
                {
                    EnviarMensajeRabbitMQ(cita);
                }

                return RedirectToAction("Index");
            }
            return View(cita);
        }

        // Método para enviar el mensaje a RabbitMQ
        private void EnviarMensajeRabbitMQ(Cita cita)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declaramos la cola "recetas"
                channel.QueueDeclare(queue: "recetas",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Construimos el mensaje a enviar
                string message = $"Cita finalizada: {cita.Id}, Paciente: {cita.Paciente}, Médico: {cita.Medico}";
                var body = Encoding.UTF8.GetBytes(message);

                // Publicamos el mensaje en la cola
                channel.BasicPublish(exchange: "",
                                     routingKey: "recetas",
                                     basicProperties: null,
                                     body: body);
                System.Diagnostics.Debug.WriteLine(" [x] Sent {0}", message);
            }
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Citas.Find(id);
            db.Citas.Remove(cita);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
