using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationModule.Controllers
{
    public class VectorController : ControllerBase
    {
            [HttpGet("exersize1")]
            public ActionResult<double> GetCosine(double[] x, double[] y)
            {
                if (x.Length != y.Length)
                {
                    return BadRequest("Вектори повинні мати однаковий розмір.");
                }

                double dotProduct = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    dotProduct += x[i] * y[i];
                }

                double xLength = Math.Sqrt(x.Sum(xi => xi * xi));
                double yLength = Math.Sqrt(y.Sum(yi => yi * yi));

                return dotProduct / (xLength * yLength);
            }
            [HttpGet("exersize2")]
            public ActionResult<double> GetCosineSum(int n, double x)
            {
                if (n < 1)
                {
                    return BadRequest(400);
                }
                double sum = 0.0;
                double cosine = Math.Cos(x);

                for (int i = 1; i <= n; i++)
                {
                    sum += Math.Pow(cosine, i);
                }

                return sum;
            }
        }
    }

