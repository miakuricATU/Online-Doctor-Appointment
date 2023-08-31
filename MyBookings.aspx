<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyBookings.aspx.cs" Inherits="DoctorAppointment.MyBookings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).read(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        })
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
      <div class="row">
         <div class="col-md-10 mx-auto">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>My Bookings</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <asp:GridView AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting" 
                            DataKeyNames="ID" class="table table-striped table-bordered" ID="GridView1" runat="server">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="Booking ID" />
                                <asp:BoundField DataField="BookingDateTime" HeaderText="Date" />
                                <asp:BoundField DataField="MemberEmailAddress" HeaderText="Email Address" />
                                <asp:BoundField DataField="ClinicLocationID" HeaderText="Clinic ID" />
                                <asp:BoundField DataField="BookingFee" HeaderText="Fee" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button  CommandName="Delete" CssClass="btn btn-danger" runat="server" Text="Cancel Booking" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>

</asp:Content>
