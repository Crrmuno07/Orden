using Orden.Helpers;
using Orden.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Orden.ViewModels
{
    public class Participant_ViewModel : GenericRepository<Participant>
    {
        readonly CommonFunctions common = new CommonFunctions();
        public ObservableCollection<Participant> Participants(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<Participant>(model.Participants.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public Participant GetParticipant(string Case, int Id)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Participants.Where(P => P.IdTicket == Case && P.IdParticipant == Id).FirstOrDefault();
            }
        }
        public int GetTitular(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Participants.Where(P => P.IdTicket == Case && P.TypeParticipant == "TITULAR").Count();
            }
        }
        public int GetUpdateTitular(string Case, int id)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Participants.Where(P => P.IdTicket == Case && P.TypeParticipant == "TITULAR" && P.IdParticipant != id).Count();
            }
        }
        public void DeleteDatagridParticipant(DataGrid dataGrid, List<Participant> list, List<Participant> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                DataGrid grid = (DataGrid)sender;
                if (MessageBox.Show(string.Format("Seguro que desea eliminar el particpante {0}", (grid.SelectedItem as Participant).IdentificationNumber), "Confirmar Eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as Participant);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
        }
        public void SaveDatagridParticipant(DataGrid dataGrid, List<Participant> list, string Type, Participant participant, int id)
        {
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            switch (Type)
            {
                case "TITULAR":
                    {
                        if (list.Where(X => X.TypeParticipant == "TITULAR").Count() == 0)
                        {
                            list.Add(participant);
                        }
                        else if (list.Where(X => X.TypeParticipant == "TITULAR" && X.IdentificationNumber == participant.IdentificationNumber && X.NameParticipant == participant.NameParticipant).Count() == 1)
                        {
                            list.Add(participant);
                        }
                        else
                        {
                            MessageBox.Show("El ticket ya cuenta con un TITULAR", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        }
                        break;
                    }
                default:
                    {
                        if (list.Where(X => X.IdentificationNumber == participant.IdentificationNumber).Count() > 0)
                        {
                            MessageBox.Show("El ticket ya cuenta con un COTITULAR con el mismo numero de documento", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        }
                        else
                        {
                            list.Add(participant);
                        }
                        break;
                    }

            }
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = list;
        }
        public string Save(string Ticket, List<Participant> list, bool validation, bool Exists)
        {
            if (list.Where(X => X.TypeParticipant == "TITULAR").Count() == 0)
            {
                return "El ticket debe de tener al menos el registro de un TITULAR";
            }
            else
            {
                if (validation == false)
                {
                    foreach (var item in list)
                    {
                        Participant participant = new Participant
                        {
                            IdTicket = Ticket,
                            IdParticipant = item.IdParticipant,
                            IdentificationType = item.IdentificationType,
                            IdentificationNumber = item.IdentificationNumber,
                            NameParticipant = item.NameParticipant,
                            TypeParticipant = item.TypeParticipant
                        };
                        if (Exists == false)
                        {
                            participant.IdParticipant = 0;
                        }
                        if (participant.IdParticipant == 0) { Add(participant); } else { Update(participant); }
                    }
                }
            }
            return "";
        }
        public void Delete(string Ticket, List<Participant> list)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in list)
                {
                    IList<Participant> participants = model.Participants.Where(X => X.IdTicket == Ticket && X.IdParticipant == item.IdParticipant).ToList();
                    model.Participants.RemoveRange(participants);
                    model.SaveChanges();
                }
            }
        }
    }
}
