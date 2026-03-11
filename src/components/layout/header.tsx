"use client"


import { Plus, Ticket, Menu, X } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useEffect } from "react";
import { usePrevent } from "@/hooks/use-prevent";
import { useToggle } from "@/hooks/use-toggle";

export default function Header() {
  const { stopPropagation, lockScroll, unlockScroll } = usePrevent();
  const { isOpen, toggle, close } = useToggle();

  const navItems = ["Strona główna", "Rankingi", "Cennik", "Kontakt"];

  useEffect(() => {
    isOpen ? lockScroll() : unlockScroll();
  }, [isOpen, lockScroll, unlockScroll]);

  const renderLinks = () =>
    navItems.map((item) => (
      <button
        key={item}
        className="relative text-sm font-bold text-slate-500 hover:text-slate-900 transition-colors group"
      >
        {item}
        <span className="absolute left-0 -bottom-1 h-[2px] w-0 bg-slate-900 transition-all duration-300 group-hover:w-full" />
      </button>
    ));

  const renderButton = () => (
    <Button className="bg-slate-900 hover:bg-slate-800 rounded-full px-5 py-5 font-bold transition-transform hover:scale-105">
      <Plus className="w-4 h-4 mr-2" />
      Dodaj produkt
    </Button>
  );

  return (
    <header className="relative bg-white border-b border-slate-200 w-full">
      <div className="mx-auto max-w-7xl px-6 py-4 flex items-center justify-between">
        
        {/* Logo */}
        <div className="group flex items-center gap-3 cursor-pointer">
          <div className="bg-slate-100 group-hover:bg-slate-900 p-2 border border-slate-200 rounded-full transition-all duration-300 group-hover:scale-110">
            <Ticket className="w-5 h-5 text-slate-700 group-hover:text-white transition-colors" />
          </div>

          <span className="text-xl font-bold text-slate-900 tracking-tight">
            Ticket
          </span>
        </div>

        {/* Desktop nav */}
        <nav className="hidden lg:flex items-center gap-10">
          {renderLinks()}
        </nav>

        {/* Desktop actions */}
        <div className="hidden lg:flex items-center gap-6">
          <button className="text-sm font-bold text-slate-500 hover:text-slate-900">
            Zaloguj się
          </button>

          {renderButton()}
        </div>

        {/* Hamburger */}
        <div className="lg:hidden">
          <Button variant="outline" size="icon" onClick={toggle}>
            {isOpen ? (
              <X className="w-5 h-5 transition-transform rotate-90" />
            ) : (
              <Menu className="w-5 h-5 transition-transform" />
            )}
          </Button>
        </div>
      </div>

      {/* Mobile nav */}
      {isOpen && (
  <div className="lg:hidden absolute top-full left-0 w-full z-50">
    <div
      onClick={stopPropagation}
      className="relative bg-white border-t border-slate-200 px-6 py-6 flex flex-col gap-6 animate-slideDown"
    >
      <div className="flex flex-col items-start gap-5 w-full">{renderLinks()}</div>

      <div className=" pt-6 flex flex-col items-start gap-4 w-full">
        <button className="text-sm font-bold text-slate-500 hover:text-slate-900">
          Zaloguj się
        </button>
        {renderButton()}
      </div>
    </div>
  </div>
)}
    </header>
  );
}