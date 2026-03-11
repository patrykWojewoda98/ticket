import { LogIn, LogOut, Plus } from "lucide-react";

export const NAV_LINKS = {
  authenticated: [
    { label: "Tickety", href: "/tickets" },
    { label: "O nas", href: "/about" },
    { label: "Kontakt", href: "/contact" },
  ],
  unauthenticated: [
    { label: "Strona główna", href: "/" },
    { label: "O nas", href: "/about" },
    { label: "Kontakt", href: "/contact" },
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
