<%@ Page Title="Home Page - Ease Appointments" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="DoctorAppointment.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section >
        <img src="imgs/banner2.jpg"  class="img-fluid"/>
    </section>

    <section>
        <div class="container">

            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Services</h2>
                        <p>
                            <b>
                                Primary Services
                            </b>
                        </p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img  src="imgs/feature-1.png" width="150px"/>
                        <h4>
                            Select Clinic
                        </h4>
                        <p class="text-justify">
                             Choose from our three conveniently located clinics, 
                            ensuring you receive care close to you.
                        </p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img src="imgs/feature-2.png" width="150px"/>
                        <h4>
                            Book an Appointment
                        </h4>
                        <p class="text-justify">
                             Schedule your visit with ease by picking a date within the next 7 days and selecting 
                                a slot between 10 am to 7 pm, tailored to your convenience.
                        </p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img src="imgs/feature-3.jpg" width="150px"/>
                        <h4>
                            Leave your Feedback
                        </h4>
                        <p class="text-justify">
                            Share your valuable experience on our dedicated feedback page, 
                            where your insights help us continuously enhance our services.
                        </p>
                    </center>
                </div>
            </div>

        </div>
    </section>
        

    <section >
        <img src="imgs/St.Clair-online-booking-INT-banner-1903.jpg" class="img-fluid"/>
    </section>

    <section>
        <div class="container">

            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Process</h2>
                        <p>
                            <b>
                                Step by Step Process
                            </b>
                        </p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img src="imgs/sign-up.png" width="150px"/>
                        <h4>
                            Sign Up
                        </h4>
                        <p class="text-justify">
                            Create an account to unlock hassle-free appointment 
                            bookings and the ability to share feedback, streamlining your healthcare journey.
                        </p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img src="imgs/feature-1.png" width="150px"/>
                        <h4>
                            Book appointment from 3 Clinic Locations
                        </h4>
                        <p class="text-justify">
                            Select your preferred clinic location from our trio of options, 
                            then effortlessly schedule your appointment within the next 7 days, tailored to your chosen slot.
                        </p> 
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img src="imgs/hospital.jpg" width="150px"/>
                        <h4>
                            Visit the Clinic on your booking time
                        </h4>
                        <p class="text-justify">
                            Arrive confidently at your chosen clinic on the designated date and time, knowing you've secured a convenient appointment that suits your schedule.
                        </p>
                    </center>
                </div>
            </div>

        </div>
    </section>

</asp:Content>
