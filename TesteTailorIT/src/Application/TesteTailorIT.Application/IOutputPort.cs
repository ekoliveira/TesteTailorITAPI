﻿namespace TesteTailorIT.Application
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handler(TUseCaseResponse response);
    }
}