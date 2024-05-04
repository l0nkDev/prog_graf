using Newtonsoft.Json;
using System;

namespace JuegoProgramacionGrafica
{
    public class ObjectCreation
    {
        public ObjectCreation()
        {
        }
        public static void Serialize(Scene scene, string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(scene, Formatting.Indented));
            }
        }

        public static void Serialize(Object3D object3d, string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(object3d, Formatting.Indented));
            }
        }

        public static T Deserialize<T>(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                return (T)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(T));
            }
        }

        public static Object3D Monitor(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            Object3D monitor = new Object3D(offset_x, offset_y, offset_z);
            Piece main = new Piece(0.0f, 0.3001f, 0.015f);

            Face screen = new Face(-0.317f, -0.1805f, 0.01f);
            screen.Tris.Add(0, new Tri(0.634f, 0.000f, 0.0f, 0.24f, 1.00f, 0.24f,
                                       0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                       0.000f, 0.000f, 0.0f, 0.24f, 0.24f, 1.00f));
            screen.Tris.Add(1, new Tri(0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                       0.000f, -0.000f, 0.0f, 0.24f, 0.24f, 1.00f,
                                       0.000f, 0.381f, 0.0f, 0.24f, 0.24f, 0.24f));
            main.Faces.Add("screen", screen);

            Face back = new Face(-0.317f, -0.2005f, -0.01f);
            back.Tris.Add(0, new Tri(0.634f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.634f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f));
            back.Tris.Add(1, new Tri(0.634f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f));
            main.Faces.Add("back", back);

            Face speakers = new Face(-0.317f, -0.2005f, 0.01f);
            speakers.Tris.Add(0, new Tri(0.634f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.634f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f));
            speakers.Tris.Add(1, new Tri(0.634f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f));
            main.Faces.Add("speakers", speakers);

            Face left = new Face(-0.317f, -0.2005f, -0.01f);
            left.Tris.Add(0, new Tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f));
            left.Tris.Add(1, new Tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f));
            main.Faces.Add("left", left);

            Face right = new Face(0.317f, -0.2005f, -0.01f);
            right.Tris.Add(0, new Tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f));
            right.Tris.Add(1, new Tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f));
            main.Faces.Add("right", right);

            Face top = new Face(-0.317f, 0.2005f, -0.01f);
            top.Tris.Add(0, new Tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            top.Tris.Add(1, new Tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                    0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            main.Faces.Add("top", top);

            Face bottom = new Face(-0.317f, -0.2005f, -0.01f);
            bottom.Tris.Add(0, new Tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            bottom.Tris.Add(1, new Tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                       0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            main.Faces.Add("bottom", bottom);

            monitor.Pieces.Add("main", main);

            Piece stand = new Piece(0.0f, 0.01f, 0.0f);

            Face front = new Face(-0.025f, 0.0f, 0.005f);
            front.Tris.Add(0, new Tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f));
            front.Tris.Add(1, new Tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.00f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f));
            stand.Faces.Add("front", front);

            Face sback = new Face(-0.025f, 0.0f, -0.005f);
            sback.Tris.Add(0, new Tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f));
            sback.Tris.Add(1, new Tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.00f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f));
            stand.Faces.Add("back", sback);

            Face sleft = new Face(-0.025f, 0.0f, -0.005f);
            sleft.Tris.Add(0, new Tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f));
            sleft.Tris.Add(1, new Tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f));
            stand.Faces.Add("left", sleft);

            Face sright = new Face(0.025f, 0.0f, -0.005f);
            sright.Tris.Add(0, new Tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f));
            sright.Tris.Add(1, new Tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f));
            stand.Faces.Add("right", sright);

            Face stop = new Face(0.0f, 0.3f, 0.0f);
            stop.Tris.Add(0, new Tri(-0.25f, 0.0f, -0.005f, 0.23f, 0.23f, 0.23f,
                                      0.25f, 0.0f, -0.005f, 0.23f, 0.23f, 0.23f,
                                     -0.25f, 0.0f,  0.005f, 0.23f, 0.23f, 0.23f));
            stop.Tris.Add(1, new Tri( 0.25f, 0.0f, -0.005f, 0.23f, 0.23f, 0.23f,
                                     -0.25f, 0.0f,  0.005f, 0.23f, 0.23f, 0.23f,
                                      0.25f, 0.0f,  0.005f, 0.23f, 0.23f, 0.23f));
            stand.Faces.Add("top", stop);

            monitor.Pieces.Add("stand", stand);

            Piece _base = new Piece();

            Face btop = new Face(-0.135f, 0.01f, -0.075f);
            btop.Tris.Add(0, new Tri(0.00f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f));
            btop.Tris.Add(1, new Tri(0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f,
                                     0.22f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f));
            _base.Faces.Add("top", btop);

            Face bbottom = new Face(-0.135f, 0.0f, -0.075f);
            bbottom.Tris.Add(0, new Tri(0.00f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f));
            bbottom.Tris.Add(1, new Tri(0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f,
                                        0.22f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f));
            _base.Faces.Add("bottom", bbottom);

            Face bleft = new Face(-0.135f, 0.0f, -0.075f);
            bleft.Tris.Add(0, new Tri(0.00f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                      0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f));
            bleft.Tris.Add(1, new Tri(0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                      0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.05f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f));
            _base.Faces.Add("left", bleft);

            Face bright = new Face(0.085f, 0.0f, -0.075f);
            bright.Tris.Add(0, new Tri(0.00f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                       0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                       0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f));
            bright.Tris.Add(1, new Tri(0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                       0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f,
                                       0.05f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f));
            _base.Faces.Add("right", bright);

            Face bfront = new Face(-0.085f, 0.0f, 0.075f);
            bfront.Tris.Add(0, new Tri(0.00f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f));
            bfront.Tris.Add(1, new Tri(0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.17f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f));
            _base.Faces.Add("front", bfront);

            Face bback = new Face(-0.135f, 0.0f, -0.075f);
            bback.Tris.Add(0, new Tri( 0.00f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f));
            bback.Tris.Add(1, new Tri( 0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f,
                                       0.27f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f));
            _base.Faces.Add("back", bback);

            monitor.Pieces.Add("base", _base);

            return monitor;
        }

        public static Object3D Pot(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            Object3D pot = new Object3D(offset_x, offset_y, offset_z);

            Piece body = new Piece();

            Face _base = new Face();
            _base.Tris.Add(0, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.Tris.Add(1, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.Tris.Add(2, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f));
            _base.Tris.Add(3, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.Tris.Add(4, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                      -0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f));
            _base.Tris.Add(5, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f,
                                       0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f));
            body.Faces.Add("base", _base);

            Face _baseF = new Face(0.0f, 0.001f, 0.0f);
            _baseF.Tris.Add(0, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.Tris.Add(1, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.Tris.Add(2, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.Tris.Add(3, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.Tris.Add(4, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                       -0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f));
            _baseF.Tris.Add(5, new Tri( 0.000f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f,
                                        0.050f, 0.000f, 0.087f, 0.1216f, 0.4118f, 0.50196f,
                                        0.100f, 0.000f, 0.000f, 0.1216f, 0.4118f, 0.50196f));
            body.Faces.Add("baseFix", _baseF);

            Face lf = new Face();
            lf.Tris.Add(0, new Tri(-0.05f, 0.000f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    0.05f, 0.000f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            lf.Tris.Add(1, new Tri(-0.05f, 0.000f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f,
                                   -0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            body.Faces.Add("lf", lf);

            Face lfl = new Face();
            lfl.Tris.Add(0, new Tri(-0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.05f, 0.000f, 0.087f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            lfl.Tris.Add(1, new Tri(-0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.20f, 0.173f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.Faces.Add("lfl", lfl);

            Face lfr = new Face();
            lfr.Tris.Add(0, new Tri( 0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.05f, 0.000f, 0.087f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            lfr.Tris.Add(1, new Tri( 0.10f, 0.000f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.20f, 0.173f, 0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, 0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.Faces.Add("lfr", lfr);

            Face lb = new Face();
            lb.Tris.Add(0, new Tri(-0.05f, 0.000f, -0.087f, 0.1516f, 0.4418f, 0.53196f,
                                    0.05f, 0.000f, -0.087f, 0.1516f, 0.4418f, 0.53196f,
                                    0.10f, 0.173f, -0.173f, 0.1516f, 0.4418f, 0.53196f));
            lb.Tris.Add(1, new Tri(-0.05f, 0.000f, -0.087f, 0.1516f, 0.4418f, 0.53196f,
                                    0.10f, 0.173f, -0.173f, 0.1516f, 0.4418f, 0.53196f,
                                   -0.10f, 0.173f, -0.173f, 0.1516f, 0.4418f, 0.53196f));
            body.Faces.Add("lb", lb);

            Face lbl = new Face();
            lbl.Tris.Add(0, new Tri(-0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.05f, 0.000f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            lbl.Tris.Add(1, new Tri(-0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.20f, 0.173f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                    -0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            body.Faces.Add("lbl", lbl);

            Face lbr = new Face();
            lbr.Tris.Add(0, new Tri( 0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                     0.05f, 0.000f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                     0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            lbr.Tris.Add(1, new Tri( 0.10f, 0.000f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                     0.20f, 0.173f,  0.000f, 0.1816f, 0.4718f, 0.57196f,
                                     0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            body.Faces.Add("lbr", lbr);

            Face uf = new Face();
            uf.Tris.Add(0, new Tri(-0.05f, 0.346f, 0.087f, 0.2716f, 0.5618f, 0.66196f,
                                    0.05f, 0.346f, 0.087f, 0.2716f, 0.5618f, 0.66196f,
                                    0.10f, 0.173f, 0.173f, 0.2716f, 0.5618f, 0.66196f));
            uf.Tris.Add(1, new Tri(-0.05f, 0.346f, 0.087f, 0.2716f, 0.5618f, 0.66196f,
                                    0.10f, 0.173f, 0.173f, 0.2716f, 0.5618f, 0.66196f,
                                   -0.10f, 0.173f, 0.173f, 0.2716f, 0.5618f, 0.66196f));
            body.Faces.Add("uf", uf);

            Face ufl = new Face();
            ufl.Tris.Add(0, new Tri(-0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.05f, 0.346f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            ufl.Tris.Add(1, new Tri(-0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.20f, 0.173f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                    -0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            body.Faces.Add("ufl", ufl);

            Face ufr = new Face();
            ufr.Tris.Add(0, new Tri( 0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                     0.05f, 0.346f, 0.087f, 0.2416f, 0.5318f, 0.63196f,
                                     0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            ufr.Tris.Add(1, new Tri( 0.10f, 0.346f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                     0.20f, 0.173f, 0.000f, 0.2416f, 0.5318f, 0.63196f,
                                     0.10f, 0.173f, 0.173f, 0.2416f, 0.5318f, 0.63196f));
            body.Faces.Add("ufr", ufr);

            Face ub = new Face();
            ub.Tris.Add(0, new Tri(-0.05f, 0.346f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    0.05f, 0.346f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            ub.Tris.Add(1, new Tri(-0.05f, 0.346f, -0.087f, 0.1816f, 0.4718f, 0.57196f,
                                    0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f,
                                   -0.10f, 0.173f, -0.173f, 0.1816f, 0.4718f, 0.57196f));
            body.Faces.Add("ub", ub);

            Face ubl = new Face();
            ubl.Tris.Add(0, new Tri(-0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.05f, 0.346f, -0.087f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            ubl.Tris.Add(1, new Tri(-0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.20f, 0.173f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                    -0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.Faces.Add("ubl", ubl);

            Face ubr = new Face();
            ubr.Tris.Add(0, new Tri( 0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.05f, 0.346f, -0.087f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            ubr.Tris.Add(1, new Tri( 0.10f, 0.346f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.20f, 0.173f,  0.000f, 0.2116f, 0.5018f, 0.60196f,
                                     0.10f, 0.173f, -0.173f, 0.2116f, 0.5018f, 0.60196f));
            body.Faces.Add("ubr", ubr);

            pot.Pieces.Add("body", body);

            return pot;
        }

        public static Object3D Desk(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            Object3D desk = new Object3D(offset_x, offset_y, offset_z);

            Piece desktop = new Piece(0.0f, 0.72f, 0.0f);

            Face top = new Face(0.0f, 0.03f, 0.0f);
            top.Tris.Add(0, new Tri(-0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                     0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                    -0.75f, 0.00f, 0.375f, 0.4f, 0.2745f, 0.1804f));
            top.Tris.Add(1, new Tri(0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                    -0.75f, 0.00f, 0.375f, 0.4f, 0.2745f, 0.1804f,
                                     0.75f, 0.00f, 0.375f, 0.4f, 0.2745f, 0.1804f));
            desktop.Faces.Add("top", top);

            Face bottom = new Face();
            bottom.Tris.Add(0, new Tri(-0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                        0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                       -0.75f, -0.03f, 0.375f, 0.28f, 0.1545f, 0.0604f));
            bottom.Tris.Add(1, new Tri(0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                       -0.75f, -0.03f, 0.375f, 0.28f, 0.1545f, 0.0604f,
                                        0.75f, -0.03f, 0.375f, 0.28f, 0.1545f, 0.0604f));
            desktop.Faces.Add("bottom", bottom);

            Face left = new Face(-0.75f, 0.0f, 0.0f);
            left.Tris.Add(0, new Tri(0.0f, 0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f));
            left.Tris.Add(1, new Tri(0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f));
            desktop.Faces.Add("left", left);

            Face right = new Face(0.75f, 0.0f, 0.0f);
            right.Tris.Add(0, new Tri(0.0f, 0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f));
            right.Tris.Add(1, new Tri(0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f));
            desktop.Faces.Add("right", right);

            Face front = new Face(0.0f, 0.0f, 0.375f);
            front.Tris.Add(0, new Tri(-0.75f, 0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                       0.75f, 0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                      -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f));
            front.Tris.Add(1, new Tri(0.75f, 0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                      -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                       0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f));
            desktop.Faces.Add("front", front);

            Face back = new Face(0.0f, 0.0f, -0.375f);
            back.Tris.Add(0, new Tri(-0.75f, 0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                      0.75f, 0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                     -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f));
            back.Tris.Add(1, new Tri(0.75f, 0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                     -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                      0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f));
            desktop.Faces.Add("back", back);

            desk.Pieces.Add("desktop", desktop);

            Piece leftFoot = new Piece(-0.67f);

            Face lin = new Face(0.03f);
            lin.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f));
            lin.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, 0.315f, 0.25f, 0.1245f, 0.0304f));
            leftFoot.Faces.Add("in", lin);

            Face lout = new Face(-0.03f);
            lout.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f));
            lout.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, 0.315f, 0.31f, 0.1845f, 0.0904f));
            leftFoot.Faces.Add("out", lout);

            Face lfront = new Face(0.0f, 0.0f, 0.315f);
            lfront.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            lfront.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            leftFoot.Faces.Add("front", lfront);

            Face lback = new Face(0.0f, 0.0f, -0.315f);
            lback.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            lback.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            leftFoot.Faces.Add("back", lback);

            Face lbottom = new Face();
            lbottom.Tris.Add(0, new Tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            lbottom.Tris.Add(1, new Tri(0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            leftFoot.Faces.Add("bottom", lbottom);

            desk.Pieces.Add("leftFoot", leftFoot);

            Piece rightFoot = new Piece(0.67f);

            Face rin = new Face(-0.03f);
            rin.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f));
            rin.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, 0.315f, 0.25f, 0.1245f, 0.0304f));
            rightFoot.Faces.Add("in", rin);

            Face rout = new Face(0.03f);
            rout.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f));
            rout.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, 0.315f, 0.31f, 0.1845f, 0.0904f));
            rightFoot.Faces.Add("out", rout);

            Face rfront = new Face(0.0f, 0.0f, 0.315f);
            rfront.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rfront.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rightFoot.Faces.Add("front", rfront);

            Face rback = new Face(0.0f, 0.0f, -0.315f);
            rback.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rback.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rightFoot.Faces.Add("back", rback);

            Face rbottom = new Face();
            rbottom.Tris.Add(0, new Tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            rbottom.Tris.Add(1, new Tri(0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            rightFoot.Faces.Add("bottom", rbottom);

            desk.Pieces.Add("rightFoot", rightFoot);

            return desk;
        }
    }
}
