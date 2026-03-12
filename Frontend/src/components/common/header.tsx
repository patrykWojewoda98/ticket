"use client";

import { Ticket, Menu, X } from "lucide-react";
import { useEffect } from "react";
import { NAV_BUTTONS, NAV_LINKS } from "../../lib/constrants";
import Link from "next/link";
import { useToggle } from "@/hooks/useToggle";
import { usePrevent } from "@/hooks/usePrevent";
import { Button } from "../ui/button";



interface HeaderProps {
  isAuthenticated: boolean;
}

export default function Header({ isAuthenticated }: HeaderProps) {
  const { stopPropagation, lockScroll, unlockScroll } = usePrevent();
  const { isOpen, toggle } = useToggle();

  useEffect(() => {
    isOpen ? lockScroll() : unlockScroll();
  }, [isOpen, lockScroll, unlockScroll]);

  const renderLinks = () =>
    NAV_LINKS[isAuthenticated ? "authenticated" : "unauthenticated"].map((link, index) => (
      <Link key={index} href={link.href} className="group relative font-bold text-slate-500 hover:text-slate-900 text-sm transition-colors">
        {link.label}
        <span className="-bottom-1 left-0 absolute bg-slate-900 w-0 group-hover:w-full h-[2px] transition-all duration-300" />
      </Link>
    ));

const renderButtons = (isPlain = false) =>
  NAV_BUTTONS[isAuthenticated ? "authenticated" : "unauthenticated"].map(
    (button, index) =>
      isPlain || index === 0 ? (
        <Link
          key={index}
          href={button.href}
          className="group relative font-bold text-slate-500 hover:text-slate-900 text-sm transition-colors"
        >
          {button.label}
          <span className="-bottom-1 left-0 absolute bg-slate-900 w-0 group-hover:w-full h-[2px] transition-all duration-300" />
        </Link>
      ) : (
        <Link
          key={index}
          href={button.href}
          className="flex items-center bg-slate-900 hover:bg-slate-800 px-5 py-2 rounded-full font-bold text-white hover:scale-95 transition-transform cursor-pointer"
        >
          {button.icon && <button.icon className="mr-2 w-4 h-4" />}
          {button.label}
        </Link>
      )
  );

  return (
    <header className="relative bg-white border-slate-200 border-b w-full">
      <div className="flex justify-between items-center mx-auto px-6 py-4 max-w-7xl">
        <div className="group flex items-center gap-3 cursor-pointer">
          <div className="bg-slate-100 p-2 border border-slate-200 rounded-full">
            <Ticket className="w-5 h-5 text-slate-700" />
          </div>

          <Link href="/" className="group relative font-bold text-slate-500 group-hover:text-slate-900 text-lg transition-colors">
            Ticket
            <span className="-bottom-1 left-0 absolute bg-slate-900 w-0 group-hover:w-full h-[2px] transition-all duration-300" />
          </Link>
        </div>

        <nav className="hidden lg:flex items-center gap-10">{renderLinks()}</nav>
        <div className="hidden lg:flex items-center gap-6">{renderButtons()}</div>

        <div className="lg:hidden">
          <Button variant="outline" size="icon" onClick={toggle}>
            {isOpen ? <X className="w-5 h-5 rotate-90 transition-transform" /> : <Menu className="w-5 h-5 transition-transform" />}
          </Button>
        </div>
      </div>

      {isOpen && (
        <div className="lg:hidden top-full left-0 z-50 absolute w-full">
          <div onClick={stopPropagation} className="relative flex flex-col bg-white px-6 py-6 border-slate-200 border-t animate-slideDown">
            <div className="flex flex-col items-start gap-5 w-full">{renderLinks()}</div>
            <div className="flex flex-col items-start gap-5 pt-5 w-full">{renderButtons(true)}</div>
          </div>
        </div>
      )}
    </header>
  );
}
