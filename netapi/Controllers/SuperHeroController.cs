using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace netapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlasemenController : ControllerBase
    {

        private static List<Klasemen> Hasil = new List<Klasemen>
            {
                new Klasemen
                {Id = 1,
                 NamaKlub = "NewCastle",
                 Main = 0,
                 Poin = 0
                },
                 new Klasemen
                {Id = 2,
                 NamaKlub = "Fulham",
                 Main = 0,
                 Poin = 0
                },
                  new Klasemen
                {Id = 3,
                 NamaKlub = "Everton",
                 Main = 0,
                 Poin = 0
                }


            };
        [HttpGet]
        public async Task<ActionResult<List<Klasemen>>> Get()
        {

              var hasil2 = Hasil.Select(k => new { k.Poin, k.NamaKlub, k.Main }).OrderByDescending(g => g.Poin);

            return Ok(hasil2);
        }

        [HttpGet("{peringkat}")]
        public async Task<ActionResult<Klasemen>> AmbilPeringkat(int peringkat)
        {
            var hasil2 = Hasil.Select(k => new { k.Poin, k.NamaKlub, k.Main }).OrderByDescending(g => g.Poin).ToList();

            var posisi = hasil2[peringkat - 1];
            return Ok(posisi);
        }

        [HttpPut]
        public async Task<ActionResult<Klasemen>> CatatPermainan(string tim1, string tim2, string score)
        {

            var klub1 = Hasil.Find(h => h.NamaKlub == tim1);
            var klub2 = Hasil.Find(h => h.NamaKlub == tim2);


            klub1.Main = klub1.Main += 1;
            klub2.Main = klub2.Main += 1;
            string[] splitted = score.Split(':');
            var skor1 = splitted[0];
            var skor2 = splitted[1];
            var a = int.Parse(skor1);
            var b = int.Parse(skor2);

            if (a > b)
            {
                klub1.Poin = klub1.Poin += 3;
            }else if (b > a)
            {
                klub2.Poin = klub2.Poin += 3;
            }
            else
            {
                klub1.Poin = klub1.Poin += 1;
                klub2.Poin = klub2.Poin += 1;

            }

            return Ok("berhasil mengupdate score");

        }
        
       

    }
}
