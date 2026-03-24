import { Home, Info, LogIn, LogOut, Mail, PersonStanding, Plus, Ticket } from "lucide-react";

export const NAV_LINKS = {
  unauthenticated: [
    {
      label: "Strona główna",
      href: "/",
      icon: Home,
      isAdmin: false,
    },
  ],

  customer: [
    {
      label: "Strona główna",
      href: "/",
      icon: Home,
      isAdmin: false,
    },
    {
      label: "Lista ticketów",
      href: "/customer/tickets",
      icon: Info,
      isAdmin: false,
    },
    {
      label: "Kontakt",
      href: "/customer/contact",
      icon: Mail,
      isAdmin: false,
    },
  ],

  admin: [
    {
      label: "Panel admina",
      href: "/admin",
      icon: PersonStanding,
      isAdmin: true,
    },
    {
      label: "Tickety",
      href: "/admin/tickets",
      icon: Ticket,
      isAdmin: true,
    },
    {
      label: "Klienci",
      href: "/admin/clients",
      icon: PersonStanding,
      isAdmin: true,
    },
  ],
};

export const NAV_BUTTONS = {
  unauthenticated: [
    {
      label: "Zaloguj się",
      href: "/customer/login",
      icon: LogIn,
      isAdmin: false,
    },
  ],

  customer: [
    {
      label: "Wyloguj się",
      href: "/logout",
      icon: LogOut,
      isAdmin: false,
    },
    {
      label: "Dodaj ticketa",
      href: "/customer/tickets/new",
      icon: Plus,
      isAdmin: false,
    },
  ],

  admin: [
    {
      label: "Wyloguj się",
      href: "/logout",
      icon: LogOut,
      isAdmin: false,
    },
    {
      label: "Dodaj klienta",
      href: "/admin/clients/new",
      icon: Plus,
      isAdmin: true,
    },
  ],
};
