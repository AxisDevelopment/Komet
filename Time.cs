using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxisDevelopment.Komet {
    public static class Time {
        public static float deltaTime { get; private set; }

        private static DateTime oldTime;

        private static List<DirectiveData> directives = new List<DirectiveData>();

        private struct DirectiveData {
            public DirectiveData(Func<float> delay, Directive directive) {
                this.delay = delay;
                this.directive = directive;
            }
            public Func<float> delay;
            public Directive directive;
            public bool running = false;
            public void SetRunning(bool running) {
                this.running = running;
            }
        }

        internal static void Initialize() {
            oldTime = DateTime.Now;
            deltaTime = 0f;
        }

        internal static void Update() {
            DateTime now = DateTime.Now;
            TimeSpan span = now - oldTime;
            deltaTime = (float) span.TotalSeconds;
        }

        public static async void ScheduleDirective(float delay, Directive directive) {
            await Task.Run(() => {
                Thread.Sleep((int) delay * 1000);
                directive.Execute();
            });
        }

    }
}
