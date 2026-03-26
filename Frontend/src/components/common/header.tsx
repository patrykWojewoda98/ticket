"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import { Ticket, Menu, X } from "lucide-react";
import { NAV_BUTTONS, NAV_LINKS } from "../../lib/constrants";
import { useToggle } from "@/hooks/useToggle";
import { usePrevent } from "@/hooks/usePrevent";
import { Button } from "../ui/button";
import { useAuth } from "@/components/common/AuthContext";
import { cn } from "@/lib/utils";
import NotificationBell from "./NotificationBell"; 

export default function Header() {
  const { isAuthenticated, isLoaded, user } = useAuth();
  const { stopPropagation, lockScroll, unlockScroll } = usePrevent();
  const { isOpen, toggle } = useToggle();

  const [role, setRole] = useState<string | null>(null);

  useEffect(() => {
    if (!isLoaded) return;

    if (!isAuthenticated) {
      setRole(null);
      return;
    }

    if (user?.role) {
      setRole(user.role);
    } else {
      const fetchUserRole = async () => {
        try {
          const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/user/${user.id}`);
          if (res.ok) {
            const data = await res.json();
            setRole(data.role);
          }
        } catch (err) {
          console.error(err);
        }
      };
      fetchUserRole();
    }
  }, [isAuthenticated, isLoaded, user]);

  useEffect(() => {
    isOpen ? lockScroll() : unlockScroll();
  }, [isOpen, lockScroll, unlockScroll]);

  if (!isLoaded) {
    return (
      <header className="relative bg-white border-slate-200 border-b w-full h-[73px]">
        <div className="flex justify-between items-center mx-auto px-6 py-4 max-w-7xl container">
          <div className="flex items-center gap-3">
            <div className="bg-slate-50 p-2 rounded-full w-9 h-9 animate-pulse" />
            <div className="bg-slate-50 rounded w-20 h-6 animate-pulse" />
          </div>
        </div>
      </header>
    );
  }

  const navKey = isAuthenticated ? (role === "Admin" ? "admin" : "customer") : "unauthenticated";
  const linkClass = "group relative font-bold text-slate-500 hover:text-slate-900 text-sm transition-colors";
  const underline = <span className="-bottom-1 left-0 absolute bg-slate-900 w-0 group-hover:w-full h-[2px] transition-all duration-300" />;

  return (
    <header className="relative bg-white border-slate-200 border-b w-full">
      <div className="flex justify-between items-center mx-auto px-6 py-4 max-w-7xl container">
        {/* LOGO */}
        <Link href="/" className="group flex items-center gap-3">
          <div className="bg-slate-100 p-2 border border-slate-200 rounded-full">
            <Ticket className="w-5 h-5 text-slate-700" />
          </div>
          <span className="font-bold text-slate-500 group-hover:text-slate-900 text-lg transition-colors">Ticket</span>
        </Link>

        {/* DESKTOP NAV */}
        <nav className="hidden lg:flex items-center gap-10">
          {NAV_LINKS[navKey].map((link, index) => (
            <Link key={index} href={link.href} className={linkClass}>
              {link.label}
              {underline}
            </Link>
          ))}
        </nav>

        {/* ACTIONS & NOTIFICATIONS */}
        <div className="hidden lg:flex items-center gap-6">
         
          {isAuthenticated && <NotificationBell />}

          {NAV_BUTTONS[navKey].map((btn, index) => {
            const isLogin = index === 0;
            return (
              <Button key={index} variant={isLogin ? "ghost" : "default"} className={cn("hover:bg-transparent p-0 h-auto font-bold", !isLogin && "rounded-full bg-slate-900 px-5 py-2 text-white hover:scale-95 hover:bg-slate-800 transition-transform")} asChild>
                <Link href={btn.href} className={isLogin ? linkClass : ""}>
                  {!isLogin && btn.icon && <btn.icon className="mr-2 w-4 h-4" />}
                  {btn.label}
                  {isLogin && underline}
                </Link>
              </Button>
            );
          })}
        </div>

        {/* MOBILE TRIGGER */}
        <div className="lg:hidden flex items-center gap-4">
          {isAuthenticated && <NotificationBell />}
          <Button variant="outline" size="icon" onClick={toggle}>
            {isOpen ? <X className="w-5 h-5 rotate-90" /> : <Menu className="w-5 h-5" />}
          </Button>
        </div>
      </div>

      {/* MOBILE MENU */}
      {isOpen && (
        <div className="lg:hidden top-full left-0 z-50 absolute w-full" onClick={stopPropagation}>
          <div className="flex flex-col gap-6 bg-white slide-in-from-top-2 px-6 py-8 border-slate-200 border-t h-[calc(100vh-73px)] animate-in">
            <div className="flex flex-col items-start gap-5">
              {NAV_LINKS[navKey].map((link, index) => (
                <Link key={index} href={link.href} className={linkClass} onClick={toggle}>
                  {link.label}
                  {underline}
                </Link>
              ))}
            </div>
            <div className="flex flex-col items-start gap-5 pt-5 border-slate-100 border-t">
              {NAV_BUTTONS[navKey].map((btn, index) => (
                <Link key={index} href={btn.href} className={linkClass} onClick={toggle}>
                  {btn.label}
                  {underline}
                </Link>
              ))}
            </div>
          </div>
        </div>
      )}
    </header>
  );
}
