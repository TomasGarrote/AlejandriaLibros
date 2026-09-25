using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BE; // Ajustá según tu namespace de entidades

namespace Servicios
{
    public class ComprobanteService
    {
        public static void GenerarComprobantePDF(Venta venta, string rutaGuardado)
        {
            // 1. Crear documento en formato A4 con márgenes
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaGuardado, FileMode.Create));
                doc.Open();

                // 2. Estilos de Fuentes
                Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                Font subTituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.DARK_GRAY);
                Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                Font cabeceraTablaFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // 3. Encabezado del Comercio
                Paragraph titulo = new Paragraph("LIBRERÍA ALEJANDRÍA", tituloFont) { Alignment = Element.ALIGN_CENTER };
                Paragraph direccion = new Paragraph("Av. Rivadavia 4900 - Caballito, CABA\nComprobante de Venta", subTituloFont) { Alignment = Element.ALIGN_CENTER };
                doc.Add(titulo);
                doc.Add(direccion);
                doc.Add(new Paragraph("\n"));

                // 4. Datos de la Venta
                doc.Add(new Paragraph($"N° Venta: {venta.NroVenta:D8}", textoFont));
                doc.Add(new Paragraph($"Fecha: {venta.Fecha:dd/MM/yyyy HH:mm}", textoFont));
                doc.Add(new Paragraph($"Atendido por DNI: {venta.DniUsuario}", textoFont));
                doc.Add(new Paragraph($"Medio de Pago: {venta.MedioDePago}", textoFont));
                doc.Add(new Paragraph("\n"));

                // 5. Tabla de Productos
                PdfPTable tabla = new PdfPTable(4);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 40f, 15f, 20f, 25f }); // Anchos relativos de columna

                // Cabeceras
                string[] cabeceras = { "Producto", "Cant.", "Precio Unit.", "Subtotal" };
                foreach (var header in cabeceras)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, cabeceraTablaFont))
                    {
                        BackgroundColor = BaseColor.BLACK,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    tabla.AddCell(cell);
                }

                // Detalle de Ítems
                foreach (var item in venta.Detalle)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(item.TituloLibro, textoFont)) { Padding = 4 });
                    tabla.AddCell(new PdfPCell(new Phrase(item.Cantidad.ToString(), textoFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                    tabla.AddCell(new PdfPCell(new Phrase($"$ {item.PrecioUnitario:N2}", textoFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4 });
                    tabla.AddCell(new PdfPCell(new Phrase($"$ {item.Subtotal:N2}", textoFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4 });
                }

                doc.Add(tabla);
                doc.Add(new Paragraph("\n"));

                // 6. Totales
                Paragraph totales = new Paragraph(
                    $"Subtotal: $ {venta.Subtotal:N2}\n" +
                    $"Descuento: $ {venta.Descuento:N2}\n" +
                    $"TOTAL FINAL: $ {venta.Total:N2}\n",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK)
                )
                { Alignment = Element.ALIGN_RIGHT };

                doc.Add(totales);

                // Pie de página
                Paragraph pie = new Paragraph("¡Gracias por su compra en Alejandría!", subTituloFont) { Alignment = Element.ALIGN_CENTER };
                doc.Add(new Paragraph("\n\n"));
                doc.Add(pie);

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el PDF: " + ex.Message);
            }
        }
    }
}
