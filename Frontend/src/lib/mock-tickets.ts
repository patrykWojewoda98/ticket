export type TicketStatus = "OPEN" | "PENDING" | "CLOSED";

export type Ticket = {
  id: number;
  title: string;
  status: TicketStatus;
  createdAt: string;
};

export const mockTickets: Ticket[] = [
  { id: 1, title: "Nie mogę się zalogować", status: "OPEN", createdAt: "2026-03-10" },
  { id: 2, title: "Problem z płatnością", status: "PENDING", createdAt: "2026-03-09" },
  { id: 3, title: "Błąd w aplikacji", status: "CLOSED", createdAt: "2026-03-07" },
  { id: 4, title: "Nie działa reset hasła", status: "OPEN", createdAt: "2026-03-11" },
  { id: 5, title: "Brak powiadomień email", status: "PENDING", createdAt: "2026-03-08" },
  { id: 6, title: "Problem z dodawaniem projektu", status: "CLOSED", createdAt: "2026-03-05" },
  { id: 7, title: "Nie mogę pobrać raportu", status: "OPEN", createdAt: "2026-03-12" },
  { id: 8, title: "Błąd przy logowaniu przez Google", status: "PENDING", createdAt: "2026-03-09" },
  { id: 9, title: "Nie widać historii ticketów", status: "CLOSED", createdAt: "2026-03-04" },
  { id: 10, title: "Problem z wyświetlaniem dashboardu", status: "OPEN", createdAt: "2026-03-13" },
  { id: 11, title: "Nie działają filtry w liście ticketów", status: "PENDING", createdAt: "2026-03-10" },
  { id: 12, title: "Błąd przy generowaniu faktury", status: "CLOSED", createdAt: "2026-03-06" },
];