using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3.Domain
{
    public class SeparationDetector : ISeparationDetector
    {
        private List<ISeparation> _formorSeparations;
        public event EventHandler<SeparationEvent> UpdatedSeparations;
        private ILogger _logger;

        public SeparationDetector(ITracksUpdated updated, ILogger logger)
        {
            _logger = logger;
            _formorSeparations = new List<ISeparation>();
            updated.TrackUpdated += CheckSeparation;
        }

        private void CheckSeparation(object sender, TrackEvents e)
        {
            var tempSeparations = new List<ISeparation>();

            foreach (var firstTrack in e.TrackData)
            {
                foreach (var secondTrack in e.TrackData)
                {
                    if (firstTrack.Tag != secondTrack.Tag)
                    {
                        if (IsTracksToClose(firstTrack, secondTrack))
                        {
                            var formorSeparations = tempSeparations.FirstOrDefault(sep =>
                                sep.FirstTag == firstTrack.Tag || sep.SecondTag == secondTrack.Tag);
                            if (formorSeparations == null) tempSeparations.Add(new Separation(firstTrack.Tag, secondTrack.Tag, firstTrack.Timestamp));
                        }
                    }
                }
            }

            foreach (var s in tempSeparations)
            {
                var formorSeparation =
                    _formorSeparations.FirstOrDefault(sep =>
                        sep.FirstTag == s.FirstTag && sep.SecondTag == s.SecondTag);
                if (formorSeparation == null)
                {
                    string logString = s.FirstTag + " Has started a seperation event with " +s.SecondTag + " ; " + s.TimeStamp.ToString();
                    _logger.Log(logString);
                }
            }

            _formorSeparations = tempSeparations;
            SeparationEventUpdater(new SeparationEvent(tempSeparations));
        }

        protected virtual void SeparationEventUpdater(SeparationEvent e)
        {
            UpdatedSeparations?.Invoke(this, e);
        }

        public bool IsTracksToClose(ITrack firstTrack, ITrack secondTrack)
        {
            if (Math.Abs(firstTrack.Altitude - secondTrack.Altitude) <= 3000)
            {
                if (Math.Abs(firstTrack.XCoordinate - secondTrack.XCoordinate) <= 50000)
                {
                    if (Math.Abs(firstTrack.YCoordinate - secondTrack.YCoordinate) <= 50000)
                        return true;

                    return false;
                }

                return false;
            }

            return false;
        }
    }
}