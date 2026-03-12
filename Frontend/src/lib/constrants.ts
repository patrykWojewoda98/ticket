import { Home, Info, LogIn, LogOut, Mail, Plus, Ticket } from "lucide-react";

export const NAV_LINKS = {
  authenticated: [
    { label: "Tickety", href: "/tickets", icon: Ticket },
    { label: "O nas", href: "/about", icon: Info },
    { label: "Kontakt", href: "/contact", icon: Mail },
  ],
  unauthenticated: [
    { label: "Strona główna", href: "/", icon: Home },
    { label: "O nas", href: "/about", icon: Info },
    { label: "Kontakt", href: "/contact", icon: Mail },
  ],
};

export const NAV_BUTTONS = {
  authenticated: [
    { label: "Wyloguj się", href: "/logout", icon: LogOut },
    { label: "Dodaj ticketa", href: "/submit", icon: Plus },
  ],
  unauthenticated: [
    { label: "Zaloguj się", href: "/login", icon: LogIn },
    { label: "Dodaj ticketa", href: "/register", icon: Plus },
  ],
};
