import { Home, Info, LogIn, LogOut, Mail, PersonStanding, Plus, Ticket } from "lucide-react";

export const NAV_LINKS = {
  authenticated: [
    { label: "Panel admina", href: "/admin", icon: PersonStanding },
    { label: "Tickety", href: "/admin/tickets", icon: Ticket },
    { label: "Klienci", href: "/admin/clients", icon: PersonStanding },
   
   
  ],
  unauthenticated: [
    { label: "Strona główna", href: "/", icon: Home },
    { label: "Lista ticketów", href: "/customer/tickets", icon: Info },
    { label: "Kontakt", href: "/customer/contact", icon: Mail },
  ],
};

export const NAV_BUTTONS = {
  authenticated: [
    { label: "Wyloguj się", href: "/logout", icon: LogOut },
    { label: "Dodaj klienta", href: "/admin/clients/new", icon: Plus },
  ],
  unauthenticated: [
    { label: "Zaloguj się", href: "/customer/login", icon: LogIn },
    { label: "Dodaj ticketa", href: "/customer/login", icon: Plus },
  ],
};
