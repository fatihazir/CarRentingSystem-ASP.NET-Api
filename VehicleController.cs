using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarRentingSystem.BusinessLogic.Concretes;
using CarRentingSystem.Commons.Concretes.Helpers;
using CarRentingSystem.Commons.Concretes.Logger;
using CarRentingSystem.DataAccess.Entity;

namespace CarRentingSystemApi.Controllers
{
    public class VehicleController : ApiController
    {
        
        [HttpGet]
        public HttpResponseMessage VehicleListGet()
        {
            try
            {
                VehicleBusiness repo = new VehicleBusiness();
                var temp = repo.ListVehicles();
                var tempVehicles = repo.ListVehicles().Select(
                    i => new
                    {
                        i.AmoutOfSeat,
                        i.Brand,
                        i.CompanyId,
                        i.Companies.Name,
                        i.Companies.PhoneNumber,
                        i.Companies.Point,
                        i.CurrentKm,
                        i.DailyPrice,
                        i.HaveAirBag,
                        i.MinimumAgeLimit,
                        i.ModelName,
                        PhotoURL = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxISERUSEhMWFRUVFxUVGBUXGRcWGBcXFRUWFxUVFhcaHSggGBolHRUWITEhJSkrMC4vFx82ODMtNygtLisBCgoKDg0OGhAQGy0lHyUtLS0tLS0xLi0tLS0tLS0tLS0tLS0rLSstLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf/AABEIAJwBQwMBEQACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAAAAgMEBQYBBwj/xABLEAABAwIDAwgGBAwFAwUBAAABAAIDBBEFITEGEkETQlFSYXGBkQciMqGxwRQjktEVFjNDU2JygrLT4fAXVIOiwpPD0kRjc6PjNP/EABoBAQADAQEBAAAAAAAAAAAAAAABAgMEBQb/xAA6EQACAQIBCQYGAQQABwAAAAAAAQIDEQQFEhQhMUFRkaETMkJSgdEiYXGx4fAVM0NTwSNiY4KS4vH/2gAMAwEAAhEDEQA/APcUAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAICE/EmNlMTzundDgToQSRroDcHJZ9rFTzHt2mnZScM9bNg8ayPrt8wVoZnPpjP1j3MefgEBz6a3oef9N/zagEOxADmP/2j4uCAZdi7RzHfahHxkTUTZifw2zqn7cH8xLriM18AGNs6jvtw/wAxLoWY4zFWnmSeADv4SUIHPwiziHjvjkH/ABQDsVbG72XtJ6Li/lqgH0AIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAICt2gxJ9NCZWxh7W3Ly5+4GNAJLyd03AtoBfNQ20tRaKTdm7GJf6RpHC8Yht0tD5ffdma4pY1RdrHoQwF1e5Bn26qX+y54/YbE3+Jj/ispZQsarJyGnYzWyaCT96V/vDN0HyXNLKvA3jk2KWs7U11bE3lJqqGnYTk54ib4NLhd3vKtTxeLq/04vokZzw+Dpd+X3ZQ1m3bRriNTLwtBG5oPcX7jbeK6VSxcu9NLm/YwdfBx7sG+S9yFLtW9zd5rax4OhlqGxC3TZoeVosJUfeqP01e5R42mu7SXr+LEKTHHH/07Cf/AHJ5pPc1rfirLBR3yk/X8FXj57oxXp7sjOxmX9DSj/Tmd/FKmg0XtvzfuV0+vua5L2EHHZxoynH+gT/FIVZYKgvD1fuQ8fiPN0XsR6raOra24dDrbKnhHAniHdCnRKHlRGm4jzsiM2rrbi74rX/QQdPZGE0Sh5ERpmI87Jjto6gZEQO74Gj+FwR4Og/D9ydNr+b7D8G2dUz2WwDuZKz3tlUaHTWy6/7pe5GmVXts/rGPsWcHpJqBk+Nzum07j4tbKxw8LqdHku7OS5P7oaQn3oRfNfZlrTekONwAMs1ORzyywN+BMBLTb9Ziq44hbGn6W910LRnh3ti11X+n1NbhW2NTu77XxVMfFw3T+7vx5A/tALJ4uVP+rG3z3c9nOxssLTqf05X+/J6y/wAD29pKh/JPJgm05OX1b/suORvwvYnoXdF5yujgnHNlZmqUlQQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBAZfaTbWCmJjj+umF7saQGs7ZX6MHZr3XU2IueSY9j9ViDt3e5Vt9LFtOw/qM1lcOkrCviadFfE9fA6KGFqVn8K1cdxYbN7Gbt3G+87UnInsDRk0LwcTjp1nZLUe3Rw1LDL5m2ocAY0XIAtmSeAGpJ4BYQw06jsxUxiitRmMY2x3iYMMa15Fw6rcLxtOn1QOUh19Y+rlzrr2sNk6nT1yV2eTWxk56kzL1WCe1UVDnTSaGSQl2eZ3QToBnkNAvRscZUYJhX0uVz7fUx+G8eDR32v2AdoQGxwzZ0zyZ5MGbrdHBo7/vViGaRuy1M3SJvjd3xKEXD8BQjSGP7DfuUg4cLYOY0fuj7kIuYfbsMP1bWD1CCXDpI9nTtChlkYnkNVUk3uzlO11O07ovc3yGt75+BCuirJUuGMOsbT3tB+SEEKbAIXfmwO67fglibldUbLN1Y5zT0HMfIpYXK12Fz0z+VbvMcPzsRINuhxGoy0IsVVolMuKfaJkrRHiEQe3hURts5va+NuY43dH2eqVxywuY86i818PC/Td6HZHFZyzayzlx3r1Nbg2KV1E1r6WUVtGdGE7zmtGu44a20y04t1VqeJ15lVZsuj+jKzw2rPpPOXVfVG/2a2ypa0AMduScYn2Du3d4O8M+kBdTRy3NEoJBACAEAIAQAgBACAEAIAQAgBACAEAIAQEGpxinjbI580YEVhJ6wJYSLhrgMw48BqUB5Xtf6SHzHkqcujjOV2/lpexvUb29ngpbUVdiKcnZFLhGzctQAZRuR3uIW6E9Mh1kd7l4+Jylf4aPM9fD5OjBZ1bl7/vM32F4AyMDIZf34LzVRlN3kdk8SkrRLOtqYKWF0072xxsFy4+4ADNzjwAzK7aOGcnZHn1a9tbPPsTxCoxU2IdT0I0i0kn6HTEaN6GDLpvkV7FKjGmtR585uT1lhSYfHG0NaAGjgMhktShkducbM72U0QAFrbrbgBpPxdbPsA6UBoNl6INpWMA0c+56TvHM+Fh3AIDfYZQiOJoHEBxPSSP7Cko2SHQKQNOpkAxLTWBJGmaA812ioSYnvIzLmk97ngfNVLmVNEb6f3/ZUA2OxEN43t6D/AH7i1WRVmgfRKxA06iQDLqPsUAQaRSCnxPAIyC9pEZHg0/d4KLE3KjCm1EEt6T2nG7ofajlsOLRo6wyc2x8ysasYSjaew2pSnGV4bTSvpYq0+ux9HVtG8Q8WOXONgBKzT122IyuBkDyxlOhsedDqjqlCFfas2fR/Qs8J23q6F4gr2GVnNkvd1us1+kg7DnnmeC7ITjUV4s4pwlB2kelYTi0NSzlIJA9vG2rT0Oac2nvUkE5ACAEAIAQAgBACAEAIAQAgBACAEBmdpNsYqV/IsaZZzpG3hfTePvt8FhWxEaS17Tqw+FlV17FxPOMUwCGWWSoqHbr5XGR0FNuhu+eL3kG5vmRmCSdbledUytmbkd0clqb1N24u32IGGYWYiXsiLnHnP9Y92g07uC83FY6VZ2b1cEephcHToLVt4mip8Rq26MHkuVVbbH0NZUactv3JseMVvUb5K+lTW/oZPC4fj1I2MQ/TBH9KpnPMRJaWSPjFzxLbFpOVr20yXbRytKnHNcb9Djq5MozldVLdTjaBugiqWj9WZnwMK3WW+NPr+DB5Kh/l6fkVJSNAILarPKxdEb9mTAtVlmm9sWUeS+FRFNR7JQRyOlP0pz3XJLmRGxOtrOHd3KVlmj5ZciP4me6cevsX2HyxQNLSydwuT+SZxtl+W7FdZYw/z5fkfxFbdKPP8F3BtZC0BvJyZADNltO4larK2Ge8o8kYj5cx4bW0/Frh3tk+TCrfymG8xV5JxXl6r3FjaqlOpt+7P/KUrKWGfjRV5KxS8HVe5120FI4Eb2o43H8QC0WOw78aKPJ2JXgZQ4xHBLE5rHsJJYQDJE3R7SfacOAVtKoedc0VeCxC/ty5MzL8EeXCzWG9xlNA7hfhIehWVem9klzRR4estsHyZbbL4dJDI/fjO64CxFnZ539knoatFJcTKUJb0aQs/wDbl/6Unx3Va5m0R5d0ahw72uHxCkEKWpiGr2jvIHxQaxs1ER0ew9zggKfaAgxixBAdc2PCx9yEooKTHYqV4kcN42NgC0EXLRfPs+IXHjKUqkFGPH3OzB1I05uUuHsTNqNr6ercHse5m5GzcdkCyUesC1zHEjnC9st49KwhTqwrKXhskbSnSnRcd92yZgG0UdUwU1U1ry72TazZDwLLfk5ewZHhbJp0q0JU32lH1RSnWjUXZ1vR+/uR63DZqB30qjle6MauZ6z4x0StGRb22t02WtHFQqr5mNfCzpP5Gr2R9LVNORFVObG/QSD8m79rqH3do0XRY5z0VlQx2Qc0nXIgqCR1ACAEAIAQAgBACAEAIAQAgKzaTF2UdLLUPOUbSRfi45Mb4uIChuyuXhBzkkjwqDFZw8zB93OJcbNc8kk3NzkL9IsvKlBSd390vc+lp4Rxja/JN+w1PtjVlwDIjcuDQfo8YBJNhdxGWdv6I8Nh2m5pcdusxqOpDuxn6ppffUel0uLQNY0OjBcAN52Y3nc52Wlzc5Lw3iaN+4XeFrecJcZpzYcmbXzs5w4Ht6bKyxFLW8zq/caLW83Rew0+vpjzXjue/wC9V0mHlfN+5fRqvFcl7CBXwNILTKD2POlj09tldYqFn8L5v3KvDVeK5Ie/C0fCaceMR+MZUaXHgxok+Eev+mdjxpod/wD0S5Di2E6nPSMdAWixaSvr6exV4OT8K5y9yRPtE1jd4yOI0ypzJ7mG66aVftXaK5pfg5qmH7NXkuTf5GaTa6CR27yjN61911PJE63TZz81arN0leaVvp7SZFOiqmqF/wDy/wDVE38Mwnqe8f8AErneLo8P3qa6JV+f76oPwnAecwe/5BRpFF8CdHqrj++ottXGdHM+yPvTtab2W5fkh0qi2p8xe+OHJH3K11uzStnvzjt/1YvP+iXXCP76D1l++o1UtG6TyUZtn5ZnUdF0sm+6iU2vExl1I060zD4tVM3/AKa5o17Vr+4+oy7Do+NK3wLPvS3/ACdfyW7WX+To/Y4ado0hkH7JPyKnPmtmd6NjU9so+qXsIL3DT6U3ufKPgVGkVFvnzZbsoPyckNvnPF0/7287+IFWWNrbpS5fgjRKT8MP36ESo5B3tiN3/wAkEL/4oyrLH114+i9h/H0n/bXN+5VYhS0jRvMgpnOs7IU8LL3FjvFjAT3Lphj6r1Np+hR5NpcGvUyWC7NMkqLytaYxcFhLo2kdvJFhJ7j3rplj5parHNHJkG9d7fvyNyzZihP5kD9mWfwtvSFZ/wArJbYoiWTIrY2WscAa8uY4t0Ot73GZJ6SQSe9ebVmp1HUj8N/ud1K8aapy+Kwy/Z2jlfvyU8D3nVxjaCe0kZ37V1U69W1s45qlGntzR87J0ZGTXRfsk28L3BWyznskzLPtqshJ2aMf5Ktkj7yR8CFLrVIePmEoS20+QuOPEWfk6xr/ANohx/3ByLG1lvTDoUHti0SGY5ikftRRyDpFr+G64fBarKE1tjyM3gqL2St9R9m3TmZT0r2doPycB8Vqso0/EmjN5Nm+5JMs6PbOjk1kLD0PaR7xdvvXTDE0p92SOeeErQ2xLynqGSDeY9rx0tIcPMLc52mto6hAIAQAgBACAo9ssA+n0j6ffDC4tcCRvNu03s5oIJB7CCNeCrKOcrGtGr2c86yfyezXqPnx+HyMLmCIutcB0c2RsciN5oNjrqvL0qnfvL1Xse/FJx7jT+UvdX6sXsxhsrZXSzBzbeywu3hc6usDbIfFc+UMTTlTUKdte123cCMNSqKblNu25N39TV8svEzD0M4diYXKHJQL7Swgwwnifd9y55Yhbl+8ysppbyY3CgNVlLEStYz7bWNuoWqFWkXVQiTYfnl0H5feuiFb4dfEspJsoXYOGSueS8h3ND3N3Te5c2xtn0G4z4L06WNTgoyS+u8554f4s6Lf03HKlsl2iKaeIA3cOUL97oHrXAWnbwS7t/r+LGboybve30/NyRyy43G7udV7B9IUZgzhQqz0qOzRGcK+nu6Sp7MXFDE39YpmEagfib7Ebx0KtGLTWsh2sIfjzwQ0iQ7+W+17WBt+neBt3roo0k9r2buJhVk1sX7zOGvliBl36ghuZBqIn37A3cuT3Zrq7/w2S+ewwcc3Xe5Kg2nlcN5peAeDtR32XJUzoSspXN4xjJXaH27UzDnKFVqLeHSp8BY2tl7FZVplHRhwFfjhJxA8grqs+BHYQEu2r3vajYfAKXUT2pEqnbY2cbtOwfmWeSKcV4Q4N72Os2rj/Qs8lDlB+EZkvM+Y4dqISReIDI6AdlvmnwNd0Zkk+8zv4y0/BlvFLQ4E2nxHYdr2t9lh8yrqtmbCksOp7WWFJtZvZCFnlb3grSOPb1WX76mTyfHc2STiUT/aps+lriPkVWVWjPvU19iyw9WPdqP1E8pDwMsffZw+IWebQ3XXUtm1t+a+g/FMdGysd2Ouw/Aj3q8dXdl/r8Gco+aDX01/k5Ph8TxeSny6zAD/ALoyrumnrlH1/KKKo07Rn6P2ZXfgGMO3qeZ8buw5js4OHvSLlH+nNr5bej1mjlnL/iRT/eRKixyup8pA2oYOOjrd4HvLT3rphlCrDVUjf6beRhLA0amuDs/nsNHg20tPU2a125J+jfkT+ydHeC9Ghi6VbuPXw3nnVsLUo95auO4uV0HOCAEAICh25xHkKGZ4Fy5pjHZyg3b+F1lXlm020bYeGdUSPE4JF83OG4+jg7ImNcsXBvYa5yRIgbcrKaklsLRae8nvr44R62Z6Br/RcsaM6r1F5SSWsjT43Oec2Adty/7IBIPfug31Xr4fId9c+vseXWyhSi7LWyvmrxq+eZ32YwPNz7+5epDJFCO045ZUn4YpCGVrD7M0zT07zZPcNz4q0sk0Gv8A4QsqVd6RJixGYWLZWyjqv9R/br6v+8nsXFVyJGzzPb8HVSypFtZysSo8WbJ6rgWP4tdl4Z/BeNUwk6LPVhWjNaiPUmyvAmREdItVErcbMytmkXOcsmaRc5y6nNIuHLqM0ZwcumaRcS1+Q7laUdbIWw4GjoS7IHQ9UsDu+osA3ksDhKmwOIDhUg4VIEHX++xWWwjeORxXVHKxZImctHGLvPhx8llmzm7RL3jHaPs2ikYPq4AAcw6VwZftAJG8O4ruoZNqtXZx1sdSTtcQdsaoaPph2bkp+RXcsmPe/wB5HFLHU3uf76j8G3FTz4qeUfqOMbu6zzvE9zVEsnSWwRxdN72iwi2npJjuPa+nk6rxYefDxsvPrYecNx6FGtnb7g6tmhdvRvNuw8O9cVPEST1Ox1TpRkviVyJjW0VQ7dcHSW9lwiEG/vEgNP1sbrtOmVje2t8vWwE4V5OFZXe794nmY2M6MVKlqW8pqXa2WT1WSVpsS070NNJYg2ILWbp4L0pYCi9z5s89Y6quHIlUPLTyiOKYPld6wD6aSEXBFvXbK+xuRzSsHkqle8W0zZZTqWtKKZ6nsTilS4Ppq0N5eImzmuLw+MEN3iS0EkEjho5vG66qNT4nSbvKNvldM5a1P4VVirJ9GjUrpOcEAIDyjafaRk88jH33I3uYGkXHqusXW7SL+S+fx9atKTUdi1Hu4OhCMU3teshwupXDJzB5NXgz0lPXd9T0lmrYNVdBGRdpB7iCphWnsaLamUNVU8mSG6/BejSpym0Y1KigiprMUbC3fe434Z3JPQ3t7V9Jh8MqSu9b/dh89iMU6rstS+/1MnW4/NIbNO4OAb7Xi7Xysuk5RuHBKqX1hE835zsh9pxtxSwFv2bq258iTbP1C15AsTf1CTwKATTYvUQOsS42yLJLnwzzCA1WGYyypFvZe0eydQP1Txb2fDVZVqMaqtI2o150XePIuKeqJG67zXzmIwzpSsfRUMRGrFNCJXLOJq2NXVrFLhdTYApzSAsmaDqZjJucZorShrKp6hYVcwm4oKrgLirqMxi4KM1i5xLMXApYXC6gHLqQcGvn8lO4bxwOJO6219STo0DVxPQr0aEqsrIpVrRpxzmV2I4rHT53O8dHkXkd+wPzY7de0X3R79DCwpL5nhV8VOq+CMrWbSSvJ3QG34n13E9pOXuXTc5iKa6pPOf4Nt8AgOtxadp9Y37HNHysUBb0GPNeAx4t0B2bP3Tqw+XeqyipKzLQnKDvFmkw3FHReqSXM6pzLf6f3kvEx2Tl31+/U9rCY7O+F7SbVTgjIXa4EEG1iDqD2Lz6cVF7XdHdN3WtamP4NTU7m7r24a0jTl6N5JHbJFI1p7yAV7VPKF9TXXaeLVwLi7x1r7dS2wuZtHJytPHhW/bd3mCWM2JBNrudbQKrypFbYP0s/wDZWOAk9/MtsLxy9dTPLm7z38m8NdvD6wPHlvOZ5BYYes6uNdRRaWbbWdNaj2eFzG7tO+o9SXtnkAgBAfOm3VPLFiE4i3S3lHktNucd6+t+K469Gm3do9bC1LwSUlf5kGlqHEeu0tcNRnY9ovwXk1qSi/hd0eipO2sW+RZqLIciFVVQbqdBcnsC9XAUbXqP0PKx9bZBepj6md08l/IdVq9I84taZjYrBou82Ayu4kmwAHf/AHdSQaem2JxCZrpZN2FrG77uVf8AWNjzJeWNBduixvexCARDsPUSMdLS1UE7IyA5weWbr3EbrRvNtvG4yugKuv5VjjDWwneFvbFnDoLX8R2glqAoKylMLg+N2QNw4ag9v9/FQSazDK4TRtk0PsuHQRr9/cVzYqkqkL70dWDrdnUtuZdUQiP5Vxb3An4L52rCqu4rn0KnFrWXVPS4cdagjvZJ/wCK58zFPakirqpbI9UT4sMww/8AqmeIcPip7LEb5LkZvES/x9R4YFhp0rIfF7R8Sp7Kt5lyI0mX+NihsrRu9mriPdI371GbXXiRGlcabA7Dxn2Z2HucCoviFw5k6VT3xYy3YN1sng5nj0EhWdSuty5hYijvuNv2GlGh+Cr29ZbY9UW7ai95Hk2OnCaTPfFllOk/ER37L1A5vuTS+KfIt8D2SRHkwOcc1NMhvJ7O+xjD8NlHMV1iqb3jsmMupnjVpWirQe8js2NOaRqCrZ6ZXNYlHYITx/vsUxjfUG7EbGsQFPCTqSdOs/mg/qtGfffsX0GFoKlD5s8HFV+1nq2IxVLSvnc6WVxDAfWeeJ1DGDi63DQBdJyk+ipXyvEdLAS49UF7yOlztQPIIC5ZsPXWDpGsjvmBLIxpcOkXOaAg4pgVVA3emgc1nWtdp4D1hduaAoqikBzaLdnD+iAsdnsQJPJP15hPZqw/Ly6FDSasyU2ndGmp5bDd4cPuXz+JodnNo9/D1u0gmSKeMONr271zSlmrYbpXJ7sNeOI8wqRxMeD5EODO4SwsqYCebNE7ye0/JenhailJWOTERtBn0AvZPEBACA8G9K2z0kNY6a7nQzEvBOe64+2O4HhwBHhJBjmwuv6rveVDhF7UXU5R2MlNgkI4Hx+9ZPDUn4TRYmqvEU20kb42etzzbyzPw960jBQVlsM5zc5ZzKqibus3uJz+77/FWKnouDYTLRYc6vZFytY+xaCLmCFw/KtbqXW1yyBHapBntj56qaplcZHEzwVMDySbu5SF9hbo32s8lAFT4bLFhjIWuI5apfKRm38lExrQe4vcgLfYptRiEEtLVML4Ymkx1LsnQyDRjXn2weLegdyAytZTOjc+GUWcwljgekG3lfj0FCBrZqXckfEdHC/i37wfciJNVHO2wuRfj38V5M8PNSdlqPbhiqbim5K4sSt6Qs+xqeVmnb0vMuYoPb0jzUdnLgy3aw8y5nQR0hVzHwLZ0XvO2CixNwEY6FFkTcIxbTLX4o4LgQpMkNqZBpI8dznD5qOzjwFx1uJTjSeUf6j/AL1Xso8CNQ8zHKofn5PF1/io7GPAi0eA83aOrH593iGH4tTsIC0eAobS1PF4PexnyCzeEpvaiVZbDjtoZjqI/s/cVlLJ9J8V9DRVWhp2MOOscfkR81H8fFbJP99C3bsizVIdzGjuutY4W3iZDq33ER2Xjb+q7sLR+NI5MVVzabZkNpJTNUiIHJlm9l3Zvd8Psr2meET8Nw91XLHTwg7rchlew1e8jp469A6EBd7XVs9LIcOoo3U8QALpdH1Fx+UdJ1eFhpYiwtZAQMWjlqKTD2SSEujfPA2+d/rYbDP9seSAlbR7X10WITzb2/EXuj5I+tGY4/UDS3m3Db+JQCdq8EaIYa6CN0cNQLmNwP1bugfqHMju7kIMTWMLHB7dQQQe0ZtPu9yEmxppQ9rXDnAOHiMwuHHQvFSO/ATs3H1Cd53TY2NjmeHavPoQTqJS2HdWlJU5NbbEanc5zHuNSwblrDMlxJsGt3WWv32yXsrD0l4TyHiavmJuCVR5eMF9zvsyuL+0EdKCV0gq027Nn04tDEEAICHi2GRVMRimbvNPmDwLTwKA8a2n9GtXTuL6Zv0iLWzbCRo6Cw+1+7fuCkGKkq3Qu3JWujd1ZGljvsuAKEFNtXV7/J9m/wD8UZIzRxB744zo97GHuc5rfmgN16Q4oDiIkhxHkJ2Mjbybo52cnZotuyxtN7g30GqA0uzdWWXnkjpaqRoP1kRaJGgi3KPbYXNzqW3yPSpIJtPWNmh5OdrJhGZHmqqHtcIy5rjGDd3rAloBaTbuyAAxGNwx1EMLqqtbCGSbrpImzTxi4uGRtZGyJtt2+6PElQSVvpGMf00vicXNkhieHkWL/U3N+3C4YCgMxRv3alh6Tn4ghN4NZBDGRpxP3qSBZpY+hAJ5CLoPkfuQByEX93QChSxlAH0Nnb5qM1EqTRxlCLX3neZ4qMyPAt2k+LFijP6Q+ZUdlDgie2qeZ8xQo3/pD5lV7Gn5UW0ir5md+iyfpFGj0+BOk1fMd+jTdf4KNGp8CdLq8fsHITdI9yjRafAtplXj0Dcm6Ao0Sn8y2m1fkFpur7lGhw+ZOnVPl++pzem6nuKjQ4cWTp8+CEcs/es5tvA6q9LDqm73M62KdWNmjF00u9JJJ0lx+07+q3OY3uwUEf0WudJM2nD4hCKhwLmx8o67gbdbIa+eiAf2biqDycD56athc/1t+YO3Q4232coWysIHVQGzjnhgEUUUDoWRPc+VsjGSOc259drnAkXsDdpGbdTbOSCrxDD4mSCenoXATb7nOndct+sF2hj3bjQfaG9fS1skBBoKKqmp681NVFMxzHcnFyzJJGOad5h3Geq1oy0N/V0Qk8tqxdh7BfwBv8yoBdbNvvCzsLh7zb5LKtBzhZG2HqKFRSewtxFnnoVwaNUh8XA9HSaU/hvtKZzdyS0kJLb2IaSLi/Ndna9vIlehGtBq90ebKhOLtZlzsrTl9XF9XuAyxgXJJ9aRrQM9cjqsqleDtFPa0aU6E1eUlayZ9MLoOYEBwlAIdKBxQEeXEGN1cEBW12NUxG7JuOHQ4Bw8igMrWDBSbuoqQnp5GMX77BAeF7RNbDWS8jYMbM58VvZDS7fjA7Gghv7qkGw2/wBnpK99LiNG3fZUxMbIbgCKSEBpMjjkwbtszxaUBzBsShu+Jjt+KJu9USNu0y7pFmx9EZeQxoz3i+5tlugWOI7Q+pyz4mNY76mpiaS2MseLxuaALNJ3XNDrZGPjvZyCgqdm562aGKkAfSTyB/LN3/VOfKGoYXFsUjRvXAAvwJBUAq9sq1ktXKYzeNm7BGelkYDb9xs49xCAnej3ZJlbO6Wpe6OnjyBYWh75MrBu80iwzJNugdNoB6XH6PcM4VNT9uH+UlwL/wAOcP8A83Vfag/kqbgP8OaD/N1P2of5SXAf4c0P+bqPOH+Wlwc/w4ov85P/APV/4pcB/hxSf5yXyj+5LgR/hxTgWFdJ4sYfmEuBp/o2j4V/nCD/ANwJcDR9GvRiDPGn/wD2S4Aejp40ro/+iR/3ClwcPo+m4VkJ72OHzKXAk7BVPCqpz38oP+JS4GXbCVvCalP+rMP+0lwI/EmuHPpz3TSfOMJcg7+KFeP0J/1fvalwMTbJYhY/Vxk8LSx9HaQlyTy6vwaejmdBUM3HhoNrhwIOjmuBs4cMuNxwUA2Po/pvpdNXUGW9PC10V/0sLt9o8bBSCDs5hjxEHVbHxRMcQA71ZJiM+TjBzyzu7RoHE2CA17Mahe1j6hrg6cNDHDSmiB3I3t6Gl287POzb84FSQQaqYzyco5hNTAN18JDXOmjhPruiA/PixuBrkelCStwBhZQ1+KTDddUOdBCON3E7xHSAS0X7CoBjYKKaZrxDFJK6wyjY55FzqQ0G2QQGm2Z2aruT3TQz33j7cTmZHteAiBqaTYXEZNYGxjpfJGPc0k+5LkF/h3ownNuWqY29jGuk97t1YSw9KW2J0RxNVKyZr8B2KgpniS7pHtzBcGgA9IAGvirQo04d2KRWdepPvM060MgQESun3W3QHnm0u1ZjJAKA84xnbeU3s4oDK1m1EzucUBWyYzKecUAwKsuPrHxQGw2J2qFNvU1Sx01JJvB8YcWuZvCxfGbjxbcX11veQX52MiqKd0OEVDJA6USPEzxHI5rQ4RM3bXG7vO1t08bAAwHYOqpS6TEjEymex0UsZnbvkEgsLdRcPa1wN8kQIWIY1S0VMaTDXSuc/OaqeS0uJFiyJgybllvDzORQGFmnDfkFAJEG0kzAGtdYDIAZAICVHtfOOcUA8NtqjrFAK/Heo6xQHfx4qOsUAfjzUdYoBX49VHWKA7+Pc/WKA7+PdR1igOjbyfrFAd/H2frFAdG30/WKA6Nv5+sUB0ekCfrFAKHpBn6xQHR6Q5+sUAoekSfrFAQcX2s+ktDZmh1r7ruc0nWx8BlobICJgOMvpZmzRm1iDkbccxfhp7uKkHpdZQUGMmGoNRLHNHZskQzD2m5BZn9Wb2BNzkDa9gUBRY/svjfLvApHFkjnBm4IntbGBZjbg+qAwNA3uhAWrdkLOpq2vqTSSQsHLstvSOdDbkzGblt3MLb9187lAZnbva99fI1jbiCK+406kk5vd2nS3DPpQETZrar6G1wj1eQXO6bXsO4XPmoBpab0lScSgLqk9I5OpQGt2e2xEpAvqgN7BJvAFAOoAQEHEYN5tkB5btXsxI9xIBQGDr9jJuqUBSVGx845pQEGTZiccwoCHNgszdWHyQEYPczJwPigJMdeBx8CA74qQLdiI6QO5oHwCAYdVFx9UEnzUAUMLmdnulAKGCy9UoDowObqlAOM2fmPNKAkN2VnPNKA6dlZ+oUAk7Lz9QoBJ2Zn6hQCTs5P1CgEnZ6bqlAJOAzdUoBJwObqlAcOCy9UoBJweXqlAc/BMvVKAT+CpeqUBz8GSdUoBJw+TqlAIdSPHNKASxzmcPBASqfECw3a4tKAuodtqto3RUSAdj3/AHqbgrq7GpJTeSRzuOZJ8hwUArpKguyCAXDRvdoCgLCnwmTqlAW9FgkpPslAehbH7PytcCQUB7JhrCGAFAS0AIDhCAakpmnUICO/DIzzQgGnYJEeaEAy7Z2E80IBp+y1OdWN8kBGk2IpHawsPgEBEk9G+HnWnZ5IBoejHDgb/R2eSAkx7AUbfZiYO4BAPfiZT9QeSA6NjYOqEB0bHwdUIB5mysA5oQEluz8I5oQAdn4eqEBw7PQ9QIBJ2ch6gQCTszB1AgEHZaDqhAJOycHVHkgEHZCDqhAIOx0HUCAQdi4OqEAk7EwdUIBJ2Hg6oQCDsLB1QgEHYKDqhANP9HtOeYEBHl9GFI7WMICJJ6IqF35seGSAZPoboeofMoDjfQzQjmn7RQEuD0UUbNIwgJ8Po9p26NCAmR7FQjmhATabZiFvNCAs4cPY3QBAS2tsgFIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEAIAQAgBACAEB/9k=",
                        i.Plate,
                        i.RequiredOldForLicense,
                        i.DatetimeOfCreated,
                        i.Id

                    }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, tempVehicles);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Vehicle List Get failed. " + "\n" + ExceptionHelper.ExceptionToString(ex));
                return null;
            }
        }

        [HttpGet]
        public string VehicleRemove(int id)
        {
            try
            {
                VehicleBusiness repo = new VehicleBusiness();
                bool result = repo.Remove(id);
                return result == true ? "Removed Succesfuly!" : "Remove Failed!";
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Vehicle Remove failed: " + id+ "\n" + ExceptionHelper.ExceptionToString(ex));
                return "Removing failed! Exception : " + ex.Message;
            }
        }

        [HttpGet]
        public HttpResponseMessage VehicleFind(int id)
        {
            try
            {
                VehicleBusiness repo = new VehicleBusiness();

                var result = repo.Find(id);

                Vehicles tempData = new Vehicles()
                {
                    AmoutOfSeat = result.AmoutOfSeat,
                    Brand = result.Brand,
                    CompanyId = result.CompanyId,
                    CurrentKm = result.CurrentKm,
                    DailyPrice = result.DailyPrice,
                    HaveAirBag = result.HaveAirBag,
                    Id = result.Id,
                    MinimumAgeLimit = result.MinimumAgeLimit,
                    ModelName = result.ModelName,
                    PhotoURL = result.PhotoURL,
                    Plate = result.Plate,
                    RequiredOldForLicense = result.RequiredOldForLicense,
                    VolumeOfLuggage = result.VolumeOfLuggage
                };



                return Request.CreateResponse(HttpStatusCode.OK, tempData);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Vehicle Find failed: " + id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return null;
            }


        }

        [HttpPost]
        public string VehicleUpdate(Vehicles entity)
        {
            try
            {
                VehicleBusiness repo = new VehicleBusiness();
                bool result = repo.UpdateVehicle(entity);
                return result == true ? "Updated Succesfuly!" : "Update Failed!";
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Vehicle Update failed: " + entity.Id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return "Updating failed! Exception : " + ex.Message;
            }
        }

      
    }
}
