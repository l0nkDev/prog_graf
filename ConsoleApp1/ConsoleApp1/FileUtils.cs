using Newtonsoft.Json;

namespace JuegoProgramacionGrafica
{
    public class FileUtils
    {
        public FileUtils()
        {
        }

        public static void Serialize(GraphicsElement elem, string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(elem, Formatting.Indented));
            }
        }

        public static GraphicsElement Deserialize(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                return JsonConvert.DeserializeObject<GraphicsElement>(sr.ReadToEnd());
            }
        }

        public static GraphicsElement Monitor(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            GraphicsElement monitor = new(1, true, offset_x, offset_y, offset_z);
            GraphicsElement main = new(1, true, 0.0f, 0.3001f, 0.015f);
            GraphicsElement screen = new(2, true, -0.317f, -0.1805f, 0.01f);
            screen.tris.Add(0, new Tri(0.634f, 0.000f, 0.0f, 0.24f, 1.00f, 0.24f,
                                       0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                       0.000f, 0.000f, 0.0f, 0.24f, 0.24f, 1.00f));
            screen.tris.Add(1, new Tri(0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                       0.000f, -0.000f, 0.0f, 0.24f, 0.24f, 1.00f,
                                       0.000f, 0.381f, 0.0f, 0.24f, 0.24f, 0.24f));
            main.children.Add("screen", screen);

            GraphicsElement back = new(2, true, -0.317f, -0.2005f, -0.01f);
            back.tris.Add(0, new Tri(0.634f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.634f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f));
            back.tris.Add(1, new Tri(0.634f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f));
            main.children.Add("back", back);

            GraphicsElement speakers = new(2, true, -0.317f, -0.2005f, 0.01f);
            speakers.tris.Add(0, new Tri(0.634f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.634f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f));
            speakers.tris.Add(1, new Tri(0.634f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f));
            main.children.Add("speakers", speakers);

            GraphicsElement left = new(2, true, -0.317f, -0.2005f, -0.01f);
            left.tris.Add(0, new Tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f));
            left.tris.Add(1, new Tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f));
            main.children.Add("left", left);

            GraphicsElement right = new(2, true, 0.317f, -0.2005f, -0.01f);
            right.tris.Add(0, new Tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f));
            right.tris.Add(1, new Tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f));
            main.children.Add("right", right);

            GraphicsElement top = new(2, true, -0.317f, 0.2005f, -0.01f);
            top.tris.Add(0, new Tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            top.tris.Add(1, new Tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                    0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            main.children.Add("top", top);

            GraphicsElement bottom = new(2, true, -0.317f, -0.2005f, -0.01f);
            bottom.tris.Add(0, new Tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            bottom.tris.Add(1, new Tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                       0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            main.children.Add("bottom", bottom);

            monitor.children.Add("main", main);

            GraphicsElement stand = new(1, true, 0.0f, 0.01f, 0.0f);
            GraphicsElement front = new(2, true, -0.025f, 0.0f, 0.005f);
            front.tris.Add(0, new Tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f));
            front.tris.Add(1, new Tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.00f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f));
            stand.children.Add("front", front);

            GraphicsElement sback = new GraphicsElement(2, true, -0.025f, 0.0f, -0.005f);
            sback.tris.Add(0, new Tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f));
            sback.tris.Add(1, new Tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.00f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f));
            stand.children.Add("back", sback);

            GraphicsElement sleft = new(2, true, -0.025f, 0.0f, -0.005f);
            sleft.tris.Add(0, new Tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f));
            sleft.tris.Add(1, new Tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f));
            stand.children.Add("left", sleft);

            GraphicsElement sright = new GraphicsElement(2, true, 0.025f, 0.0f, -0.005f);
            sright.tris.Add(0, new Tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f));
            sright.tris.Add(1, new Tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f));
            stand.children.Add("right", sright);

            GraphicsElement stop = new GraphicsElement(2, true, 0.0f, 0.3f, 0.0f);
            stop.tris.Add(0, new Tri(-0.025f, 0.0f, -0.005f, 0.23f, 0.23f, 0.23f,
                                      0.025f, 0.0f, -0.005f, 0.23f, 0.23f, 0.23f,
                                     -0.025f, 0.0f,  0.005f, 0.23f, 0.23f, 0.23f));
            stop.tris.Add(1, new Tri( 0.025f, 0.0f, -0.005f, 0.23f, 0.23f, 0.23f,
                                     -0.025f, 0.0f,  0.005f, 0.23f, 0.23f, 0.23f,
                                      0.025f, 0.0f,  0.005f, 0.23f, 0.23f, 0.23f));
            stand.children.Add("top", stop);

            monitor.children.Add("stand", stand);

            GraphicsElement _base = new GraphicsElement(1);
            GraphicsElement btop = new GraphicsElement(2, true, -0.135f, 0.01f, -0.075f);
            btop.tris.Add(0, new Tri(0.00f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f));
            btop.tris.Add(1, new Tri(0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f,
                                     0.22f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f));
            _base.children.Add("top", btop);

            GraphicsElement bbottom = new GraphicsElement(2, true, -0.135f, 0.0f, -0.075f);
            bbottom.tris.Add(0, new Tri(0.00f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f));
            bbottom.tris.Add(1, new Tri(0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f,
                                        0.22f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f));
            _base.children.Add("bottom", bbottom);

            GraphicsElement bleft = new GraphicsElement(2, true, -0.135f, 0.0f, -0.075f);
            bleft.tris.Add(0, new Tri(0.00f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                      0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f));
            bleft.tris.Add(1, new Tri(0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                      0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.05f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f));
            _base.children.Add("left", bleft);

            GraphicsElement bright = new GraphicsElement(2, true, 0.085f, 0.0f, -0.075f);
            bright.tris.Add(0, new Tri(0.00f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                       0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                       0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f));
            bright.tris.Add(1, new Tri(0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                       0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f,
                                       0.05f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f));
            _base.children.Add("right", bright);

            GraphicsElement bfront = new GraphicsElement(2, true, -0.085f, 0.0f, 0.075f);
            bfront.tris.Add(0, new Tri(0.00f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f));
            bfront.tris.Add(1, new Tri(0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.17f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f));
            _base.children.Add("front", bfront);

            GraphicsElement bback = new GraphicsElement(2, true, -0.135f, 0.0f, -0.075f);
            bback.tris.Add(0, new Tri( 0.00f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f));
            bback.tris.Add(1, new Tri( 0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.27f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f));
            _base.children.Add("back", bback);

            monitor.children.Add("base", _base);

            return monitor;
        }

        public static GraphicsElement Pot(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            GraphicsElement pot = new(1, true, offset_x, offset_y, offset_z);
            GraphicsElement body = new(1);

            GraphicsElement _base = new(2);
            _base.level = 2;
            _base.tris.Add(0, new Tri( 0.000f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.100f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.tris.Add(1, new Tri( 0.000f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.tris.Add(2, new Tri( 0.000f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.100f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f));
            _base.tris.Add(3, new Tri( 0.000f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.100f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.tris.Add(4, new Tri( 0.000f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.tris.Add(5, new Tri( 0.000f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.100f, 0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f));
            body.children.Add("base", _base);

            GraphicsElement _baseF = new(2, true, 0.0f, 0.001f, 0.0f);
            _baseF.tris.Add(0, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.tris.Add(1, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.tris.Add(2, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.tris.Add(3, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.tris.Add(4, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.tris.Add(5, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f));
            body.children.Add("baseFix", _baseF);

            GraphicsElement lf = new();
            lf.level = 2;
            lf.tris.Add(0, new Tri(-0.05f, 0.000f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    0.05f, 0.000f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            lf.tris.Add(1, new Tri(-0.05f, 0.000f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f,
                                   -0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            body.children.Add("lf", lf);

            GraphicsElement lfl = new();
            lfl.level = 2;
            lfl.tris.Add(0, new Tri(-0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.05f, 0.000f, 0.087f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            lfl.tris.Add(1, new Tri(-0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.20f, 0.173f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.children.Add("lfl", lfl);

            GraphicsElement lfr = new();
            lfr.level = 2;
            lfr.tris.Add(0, new Tri( 0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.05f, 0.000f, 0.087f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            lfr.tris.Add(1, new Tri( 0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.20f, 0.173f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.children.Add("lfr", lfr);

            GraphicsElement lb = new() { level = 2 };
            lb.tris.Add(0, new Tri(-0.05f, 0.000f, -0.087f, 0.1516f, 0.4418f, 0.53196f,
                                    0.05f, 0.000f, -0.087f, 0.1516f, 0.4418f, 0.53196f,
                                    0.10f, 0.173f, -0.173f, 0.1516f, 0.4418f, 0.53196f));
            lb.tris.Add(1, new Tri(-0.05f, 0.000f, -0.087f, 0.1516f, 0.4418f, 0.53196f,
                                    0.10f, 0.173f, -0.173f, 0.1516f, 0.4418f, 0.53196f,
                                   -0.10f, 0.173f, -0.173f, 0.1516f, 0.4418f, 0.53196f));
            body.children.Add("lb", lb);

            GraphicsElement lbl = new() { level = 2 };
            lbl.tris.Add(0, new Tri(-0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.05f, 0.000f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            lbl.tris.Add(1, new Tri(-0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.20f, 0.173f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            body.children.Add("lbl", lbl);

            GraphicsElement lbr = new() { level = 2 };
            lbr.tris.Add(0, new Tri( 0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                     0.05f, 0.000f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                     0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            lbr.tris.Add(1, new Tri( 0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                     0.20f, 0.173f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                     0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            body.children.Add("lbr", lbr);

            GraphicsElement uf = new() { level = 2 };
            uf.tris.Add(0, new Tri(-0.05f, 0.346f, 0.087f, 0.2716f, 0.5618f, 0.66196f,
                                    0.05f, 0.346f, 0.087f, 0.2716f, 0.5618f, 0.66196f,
                                    0.10f, 0.173f, 0.173f, 0.2716f, 0.5618f, 0.66196f));
            uf.tris.Add(1, new Tri(-0.05f, 0.346f, 0.087f, 0.2716f, 0.5618f, 0.66196f,
                                    0.10f, 0.173f, 0.173f, 0.2716f, 0.5618f, 0.66196f,
                                   -0.10f, 0.173f, 0.173f, 0.2716f, 0.5618f, 0.66196f));
            body.children.Add("uf", uf);

            GraphicsElement ufl = new() { level = 2 };
            ufl.tris.Add(0, new Tri(-0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.05f, 0.346f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            ufl.tris.Add(1, new Tri(-0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.20f, 0.173f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            body.children.Add("ufl", ufl);

            GraphicsElement ufr = new() { level = 2 };
            ufr.tris.Add(0, new Tri( 0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                     0.05f, 0.346f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                     0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            ufr.tris.Add(1, new Tri( 0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                     0.20f, 0.173f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                     0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            body.children.Add("ufr", ufr);

            GraphicsElement ub = new() { level = 2 };
            ub.tris.Add(0, new Tri(-0.05f, 0.346f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    0.05f, 0.346f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            ub.tris.Add(1, new Tri(-0.05f, 0.346f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f,
                                   -0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            body.children.Add("ub", ub);

            GraphicsElement ubl = new() { level = 2 };
            ubl.tris.Add(0, new Tri(-0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.05f, 0.346f, -0.087f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            ubl.tris.Add(1, new Tri(-0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.20f, 0.173f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.children.Add("ubl", ubl);

            GraphicsElement ubr = new() { level = 2 };
            ubr.tris.Add(0, new Tri( 0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.05f, 0.346f, -0.087f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            ubr.tris.Add(1, new Tri( 0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.20f, 0.173f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.children.Add("ubr", ubr);

            pot.children.Add("body", body);

            return pot;
        }

        public static GraphicsElement Desk(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            GraphicsElement desk = new(1, true, offset_x, offset_y, offset_z);

            GraphicsElement desktop = new(1, true, 0.0f, 0.72f, 0.0f);

            GraphicsElement top = new(2, true, 0.0f, 0.03f, 0.0f);
            top.tris.Add(0, new Tri(-0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                     0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                    -0.75f, 0.00f,  0.375f, 0.4f, 0.2745f, 0.1804f));
            top.tris.Add(1, new Tri( 0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                    -0.75f, 0.00f,  0.375f, 0.4f, 0.2745f, 0.1804f,
                                     0.75f, 0.00f,  0.375f, 0.4f, 0.2745f, 0.1804f));
            desktop.children.Add("top", top);

            GraphicsElement bottom = new(2);
            bottom.tris.Add(0, new Tri(-0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                        0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                       -0.75f, -0.03f,  0.375f, 0.28f, 0.1545f, 0.0604f));
            bottom.tris.Add(1, new Tri( 0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                       -0.75f, -0.03f,  0.375f, 0.28f, 0.1545f, 0.0604f,
                                        0.75f, -0.03f,  0.375f, 0.28f, 0.1545f, 0.0604f));
            desktop.children.Add("bottom", bottom);

            GraphicsElement left = new(2, true, -0.75f, 0.0f, 0.0f);
            left.tris.Add(0, new Tri(0.0f,  0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f));
            left.tris.Add(1, new Tri(0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f));
            desktop.children.Add("left", left);

            GraphicsElement right = new(2, true, 0.75f, 0.0f, 0.0f);
            right.tris.Add(0, new Tri(0.0f,  0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f));
            right.tris.Add(1, new Tri(0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f));
            desktop.children.Add("right", right);

            GraphicsElement front = new(2, true, 0.0f, 0.0f, 0.375f);
            front.tris.Add(0, new Tri(-0.75f,  0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                       0.75f,  0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                      -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f));
            front.tris.Add(1, new Tri( 0.75f,  0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                      -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                       0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f));
            desktop.children.Add("front", front);

            GraphicsElement back = new(2, true, 0.0f, 0.0f, -0.375f);
            back.tris.Add(0, new Tri(-0.75f,  0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                      0.75f,  0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                     -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f));
            back.tris.Add(1, new Tri( 0.75f,  0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                     -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                      0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f));
            desktop.children.Add("back", back);

            desk.children.Add("desktop", desktop);

            GraphicsElement leftFoot = new(1, true, -0.67f);

            GraphicsElement lin = new(2, true, 0.03f);
            lin.tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f));
            lin.tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, 0.315f, 0.25f, 0.1245f, 0.0304f));
            leftFoot.children.Add("in", lin);

            GraphicsElement lout = new(2, true, -0.03f);
            lout.tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f));
            lout.tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, 0.315f, 0.31f, 0.1845f, 0.0904f));
            leftFoot.children.Add("out", lout);

            GraphicsElement lfront = new(2, true, 0.0f, 0.0f, 0.315f);
            lfront.tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            lfront.tris.Add(1, new Tri( 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            leftFoot.children.Add("front", lfront);

            GraphicsElement lback = new(2, true, 0.0f, 0.0f, -0.315f);
            lback.tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            lback.tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            leftFoot.children.Add("back", lback);

            GraphicsElement lbottom = new(2);
            lbottom.tris.Add(0, new Tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            lbottom.tris.Add(1, new Tri(0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            leftFoot.children.Add("bottom", lbottom);

            desk.children.Add("leftFoot", leftFoot);

            GraphicsElement rightFoot = new(1, true, 0.67f);

            GraphicsElement rin = new(2, true, -0.03f);
            rin.tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f));
            rin.tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, 0.315f, 0.25f, 0.1245f, 0.0304f));
            rightFoot.children.Add("in", rin);

            GraphicsElement rout = new(2, true, 0.03f);
            rout.tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f));
            rout.tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, 0.315f, 0.31f, 0.1845f, 0.0904f));
            rightFoot.children.Add("out", rout);

            GraphicsElement rfront = new(2, true, 0.0f, 0.0f, 0.315f);
            rfront.tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rfront.tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rightFoot.children.Add("front", rfront);

            GraphicsElement rback = new(2, true, 0.0f, 0.0f, -0.315f);
            rback.tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rback.tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rightFoot.children.Add("back", rback);

            GraphicsElement rbottom = new(2);
            rbottom.tris.Add(0, new Tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            rbottom.tris.Add(1, new Tri(0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            rightFoot.children.Add("bottom", rbottom);

            desk.children.Add("rightFoot", rightFoot);

            return desk;
        }

        public static GraphicsElement Ball(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            GraphicsElement ball = new(1, true, offset_x, offset_y, offset_z);
            GraphicsElement body = new(1);

            GraphicsElement _base = new();
            _base.level = 2;
            _base.tris.Add(0, new Tri(0.000f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                     -0.100f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                     -0.050f, 0.000f, -0.087f, 0.50196f, 0.50196f, 0.50196f));
            _base.tris.Add(1, new Tri(0.000f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                     -0.050f, 0.000f, -0.087f, 0.50196f, 0.50196f, 0.50196f,
                                      0.050f, 0.000f, -0.087f, 0.50196f, 0.50196f, 0.50196f));
            _base.tris.Add(2, new Tri(0.000f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                      0.050f, 0.000f, -0.087f, 0.50196f, 0.50196f, 0.50196f,
                                      0.100f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f));
            _base.tris.Add(3, new Tri(0.000f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                     -0.100f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                     -0.050f, 0.000f,  0.087f, 0.50196f, 0.50196f, 0.50196f));
            _base.tris.Add(4, new Tri(0.000f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                     -0.050f, 0.000f,  0.087f, 0.50196f, 0.50196f, 0.50196f,
                                      0.050f, 0.000f,  0.087f, 0.50196f, 0.50196f, 0.50196f));
            _base.tris.Add(5, new Tri(0.000f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f,
                                      0.050f, 0.000f,  0.087f, 0.50196f, 0.50196f, 0.50196f,
                                      0.100f, 0.000f,  0.000f, 0.50196f, 0.50196f, 0.50196f));
            body.children.Add("base", _base);

            GraphicsElement _top = new(2, true, 0.0f, 0.346f, 0.0f);
            _top.tris.Add(0, new Tri(0.000f, 0.000f,  0.000f, 0.69196f, 0.69196f, 0.69196f,
                                    -0.100f, 0.000f,  0.000f, 0.69196f, 0.69196f, 0.69196f,
                                    -0.050f, 0.000f, -0.087f, 0.69196f, 0.69196f, 0.69196f));
            _top.tris.Add(1, new Tri(0.000f, 0.000f,  0.000f, 0.69196f, 0.69196f, 0.69196f,
                                    -0.050f, 0.000f, -0.087f, 0.69196f, 0.69196f, 0.69196f,
                                     0.050f, 0.000f, -0.087f, 0.69196f, 0.69196f, 0.69196f));
            _top.tris.Add(2, new Tri(0.000f, 0.000f,  0.000f, 0.69196f, 0.69196f, 0.69196f,
                                     0.050f, 0.000f, -0.087f, 0.69196f, 0.69196f, 0.69196f,
                                     0.100f, 0.000f, 0.000f, 0.69196f, 0.69196f, 0.69196f));
            _top.tris.Add(3, new Tri(0.000f, 0.000f, 0.000f, 0.69196f, 0.69196f, 0.69196f,
                                    -0.100f, 0.000f, 0.000f, 0.69196f, 0.69196f, 0.69196f,
                                    -0.050f, 0.000f, 0.087f, 0.69196f, 0.69196f, 0.69196f));
            _top.tris.Add(4, new Tri(0.000f, 0.000f, 0.000f, 0.69196f, 0.69196f, 0.69196f,
                                    -0.050f, 0.000f, 0.087f, 0.69196f, 0.69196f, 0.69196f,
                                     0.050f, 0.000f, 0.087f, 0.69196f, 0.69196f, 0.69196f));
            _top.tris.Add(5, new Tri(0.000f, 0.000f, 0.000f, 0.69196f, 0.69196f, 0.69196f,
                                     0.050f, 0.000f, 0.087f, 0.69196f, 0.69196f, 0.69196f,
                                     0.100f, 0.000f, 0.000f, 0.69196f, 0.69196f, 0.69196f));
            body.children.Add("top", _top);

            GraphicsElement lf = new();
            lf.level = 2;
            lf.tris.Add(0, new Tri(-0.05f, 0.000f, 0.087f, 0.63196f, 0.63196f, 0.63196f,
                                    0.05f, 0.000f, 0.087f, 0.63196f, 0.63196f, 0.63196f,
                                    0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f));
            lf.tris.Add(1, new Tri(-0.05f, 0.000f, 0.087f, 0.63196f, 0.63196f, 0.63196f,
                                    0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f,
                                   -0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f));
            body.children.Add("lf", lf);

            GraphicsElement lfl = new();
            lfl.level = 2;
            lfl.tris.Add(0, new Tri(-0.10f, 0.000f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.05f, 0.000f, 0.087f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.10f, 0.173f, 0.173f, 0.60196f, 0.60196f, 0.60196f));
            lfl.tris.Add(1, new Tri(-0.10f, 0.000f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.20f, 0.173f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.10f, 0.173f, 0.173f, 0.60196f, 0.60196f, 0.60196f));
            body.children.Add("lfl", lfl);

            GraphicsElement lfr = new();
            lfr.level = 2;
            lfr.tris.Add(0, new Tri(0.10f, 0.000f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                     0.05f, 0.000f, 0.087f, 0.60196f, 0.60196f, 0.60196f,
                                     0.10f, 0.173f, 0.173f, 0.60196f, 0.60196f, 0.60196f));
            lfr.tris.Add(1, new Tri(0.10f, 0.000f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                     0.20f, 0.173f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                     0.10f, 0.173f, 0.173f, 0.60196f, 0.60196f, 0.60196f));
            body.children.Add("lfr", lfr);

            GraphicsElement lb = new() { level = 2 };
            lb.tris.Add(0, new Tri(-0.05f, 0.000f, -0.087f, 0.53196f, 0.53196f, 0.53196f,
                                    0.05f, 0.000f, -0.087f, 0.53196f, 0.53196f, 0.53196f,
                                    0.10f, 0.173f, -0.173f, 0.53196f, 0.53196f, 0.53196f));
            lb.tris.Add(1, new Tri(-0.05f, 0.000f, -0.087f, 0.53196f, 0.53196f, 0.53196f,
                                    0.10f, 0.173f, -0.173f, 0.53196f, 0.53196f, 0.53196f,
                                   -0.10f, 0.173f, -0.173f, 0.53196f, 0.53196f, 0.53196f));
            body.children.Add("lb", lb);

            GraphicsElement lbl = new() { level = 2 };
            lbl.tris.Add(0, new Tri(-0.10f, 0.000f, 0.000f, 0.57196f, 0.57196f, 0.57196f,
                                    -0.05f, 0.000f, -0.087f, 0.57196f, 0.57196f, 0.57196f,
                                    -0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f));
            lbl.tris.Add(1, new Tri(-0.10f, 0.000f, 0.000f, 0.57196f, 0.57196f, 0.57196f,
                                    -0.20f, 0.173f, 0.000f, 0.57196f, 0.57196f, 0.57196f,
                                    -0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f));
            body.children.Add("lbl", lbl);

            GraphicsElement lbr = new() { level = 2 };
            lbr.tris.Add(0, new Tri(0.10f, 0.000f, 0.000f, 0.57196f, 0.57196f, 0.57196f,
                                     0.05f, 0.000f, -0.087f, 0.57196f, 0.57196f, 0.57196f,
                                     0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f));
            lbr.tris.Add(1, new Tri(0.10f, 0.000f, 0.000f, 0.57196f, 0.57196f, 0.57196f,
                                     0.20f, 0.173f, 0.000f, 0.57196f, 0.57196f, 0.57196f,
                                     0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f));
            body.children.Add("lbr", lbr);

            GraphicsElement uf = new() { level = 2 };
            uf.tris.Add(0, new Tri(-0.05f, 0.346f, 0.087f, 0.66196f, 0.66196f, 0.66196f,
                                    0.05f, 0.346f, 0.087f, 0.66196f, 0.66196f, 0.66196f,
                                    0.10f, 0.173f, 0.173f, 0.66196f, 0.66196f, 0.66196f));
            uf.tris.Add(1, new Tri(-0.05f, 0.346f, 0.087f, 0.66196f, 0.66196f, 0.66196f,
                                    0.10f, 0.173f, 0.173f, 0.66196f, 0.66196f, 0.66196f,
                                   -0.10f, 0.173f, 0.173f, 0.66196f, 0.66196f, 0.66196f));
            body.children.Add("uf", uf);

            GraphicsElement ufl = new() { level = 2 };
            ufl.tris.Add(0, new Tri(-0.10f, 0.346f, 0.000f, 0.63196f, 0.63196f, 0.63196f,
                                    -0.05f, 0.346f, 0.087f, 0.63196f, 0.63196f, 0.63196f,
                                    -0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f));
            ufl.tris.Add(1, new Tri(-0.10f, 0.346f, 0.000f, 0.63196f, 0.63196f, 0.63196f,
                                    -0.20f, 0.173f, 0.000f, 0.63196f, 0.63196f, 0.63196f,
                                    -0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f));
            body.children.Add("ufl", ufl);

            GraphicsElement ufr = new() { level = 2 };
            ufr.tris.Add(0, new Tri(0.10f, 0.346f, 0.000f, 0.63196f, 0.63196f, 0.63196f,
                                     0.05f, 0.346f, 0.087f, 0.63196f, 0.63196f, 0.63196f,
                                     0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f));
            ufr.tris.Add(1, new Tri(0.10f, 0.346f, 0.000f, 0.63196f, 0.63196f, 0.63196f,
                                     0.20f, 0.173f, 0.000f, 0.63196f, 0.63196f, 0.63196f,
                                     0.10f, 0.173f, 0.173f, 0.63196f, 0.63196f, 0.63196f));
            body.children.Add("ufr", ufr);

            GraphicsElement ub = new() { level = 2 };
            ub.tris.Add(0, new Tri(-0.05f, 0.346f, -0.087f, 0.57196f, 0.57196f, 0.57196f,
                                    0.05f, 0.346f, -0.087f, 0.57196f, 0.57196f, 0.57196f,
                                    0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f));
            ub.tris.Add(1, new Tri(-0.05f, 0.346f, -0.087f, 0.57196f, 0.57196f, 0.57196f,
                                    0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f,
                                   -0.10f, 0.173f, -0.173f, 0.57196f, 0.57196f, 0.57196f));
            body.children.Add("ub", ub);

            GraphicsElement ubl = new() { level = 2 };
            ubl.tris.Add(0, new Tri(-0.10f, 0.346f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.05f, 0.346f, -0.087f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.10f, 0.173f, -0.173f, 0.60196f, 0.60196f, 0.60196f));
            ubl.tris.Add(1, new Tri(-0.10f, 0.346f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.20f, 0.173f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                    -0.10f, 0.173f, -0.173f, 0.60196f, 0.60196f, 0.60196f));
            body.children.Add("ubl", ubl);

            GraphicsElement ubr = new() { level = 2 };
            ubr.tris.Add(0, new Tri(0.10f, 0.346f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                     0.05f, 0.346f, -0.087f, 0.60196f, 0.60196f, 0.60196f,
                                     0.10f, 0.173f, -0.173f, 0.60196f, 0.60196f, 0.60196f));
            ubr.tris.Add(1, new Tri(0.10f, 0.346f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                     0.20f, 0.173f, 0.000f, 0.60196f, 0.60196f, 0.60196f,
                                     0.10f, 0.173f, -0.173f, 0.60196f, 0.60196f, 0.60196f));
            body.children.Add("ubr", ubr);

            ball.children.Add("body", body);

            ball.SetScale(0.25f, 0.25f, 0.25f);
            return ball;
        }

        public static void SerializeExamples(string path)
        {
            GraphicsElement tmp = new() { level = 0 };
            GraphicsElement line = new() { level = 1 };
            tmp.children.Add("monitor", Monitor(0.25f, 0.75f));
            tmp.children.Add("pot", Pot(-0.50f, 0.75f));
            tmp.children.Add("desk", Desk());
            line.children.Add("aux", Ball(1.5f));
            line.children["aux"].children["body"].SetPosition(1.0f, 0.0f, 0.0f);
            tmp.children.Add("ball", line);
            line.SetPosition(0.0f, 1.36f, 0.2f);
            line.SetRotation(0.0f, 337f, 0.0f);
            Serialize(Pot(),     path + "pot.json");
            Serialize(Desk(),    path + "desk.json");
            Serialize(Ball(),    path + "ball.json");
            Serialize(Monitor(), path + "monitor.json");
            Serialize(tmp,       path + "main scene.json");
        }
    }
}
