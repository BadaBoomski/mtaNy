using System;
using System.Collections.Generic;
using ConsoleApp3;
using ConsoleApp3.Domain;
using NUnit.Framework;
using NSubstitute; // Tools, NUGET Pack-Man, Install-Package NSubstitute+Install-Package NSubstitute.Analyzers.CSharp


namespace UnitTestProject1
{
    [TestFixture]
    public class TestCheckTracks
    {

        private IOurAirspace _ourAirspace;
        private ITransponderReceiverClient _transponderReceiverClient;
        private List<ITrack> _trackList;


        [SetUp]
        public void Setup()
        {
            // Using NSubstitute to gain control.
            _ourAirspace = Substitute.For<IOurAirspace>();
            _transponderReceiverClient = Substitute.For<ITransponderReceiverClient>()
        }