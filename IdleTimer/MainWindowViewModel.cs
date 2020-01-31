using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using System.Windows.Threading;

namespace IdleTimer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private uint idleTime;
        private Timer timer;

        public MainWindowViewModel()
        {
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            idleTime = IdleTimeHelper.GetIdleTime();
            Debug.WriteLine("New idle time: {0}", idleTime);

            RaisePropertyChanged(nameof(IdleTime));
        }

        public TimeSpan IdleTime
        {
            get
            {
                return TimeSpan.FromMilliseconds(this.idleTime);
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
