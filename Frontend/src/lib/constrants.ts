import { Home, Info, LogIn, LogOut, Mail, PersonStanding, Plus, Ticket } from "lucide-react";

export const NAV_LINKS = {
  unauthenticated: [
    {
      label: "Strona główna",
      href: "/",
      icon: Home,
      isAdmin: false,
    },
    {
      label: "Kotankt",
      href: "https://editit.pl/#kontakt",
      icon: Home,
      isAdmin: false,
    },
    {
      label: "Oferta",
      href: "https://editit.pl/uslugi-informatyczne-edit/",
      icon: Home,
      isAdmin: false,
    },
    {
      label: "Usługi",
      href: "https://editit.pl/#Uslugi",
      icon: Home,
      isAdmin: false,
    },
  ],

  customer: [
    {
      label: "Lista ticketów",
      href: "/",
      icon: Home,
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
    {
      label: "Firmy",
      href: "/admin/company",
      icon: PersonStanding,
      isAdmin: true,
    },
    {
      label: "Opcje",
      href: "/admin/settings",
      icon: PersonStanding,
      isAdmin: true,
    },
    {
      label: "Kontakt",
      href: "/admin/contact",
      icon: Mail,
      isAdmin: false,
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
    {
      label: "Dodaj ticketa",
      href: "/customer/login",
      icon: Plus,
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
