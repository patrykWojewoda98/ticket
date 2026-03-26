import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export function proxy(request: NextRequest) {
  const role = request.cookies.get("user_role")?.value;
  const { pathname } = request.nextUrl;

  // 1. BLOKADA STRONY GŁÓWNEJ DLA ADMINA
  if (pathname === "/" && role === "Admin") {
    return NextResponse.redirect(new URL("/admin", request.url));
  }

  // 2. JEŚLI NIE JESTEŚ ZALOGOWANY
  if (!role) {
    if (pathname.startsWith("/admin") || (pathname.startsWith("/customer") && pathname !== "/customer/login")) {
      return NextResponse.redirect(new URL("/customer/login", request.url));
    }
    return NextResponse.next();
  }

  // 3. OCHRONA TRAS ADMINA
  if (pathname.startsWith("/admin")) {
    if (role !== "Admin") {
      return NextResponse.redirect(new URL("/customer/tickets", request.url));
    }
  }

  // 4. OCHRONA TRAS CUSTOMERA (Admin też nie może tu wchodzić)
  if (pathname.startsWith("/customer") && pathname !== "/customer/login") {
    if (role === "Admin") {
      return NextResponse.redirect(new URL("/admin", request.url));
    }
  }

  // 5. BLOKADA STRONY LOGOWANIA DLA ZALOGOWANYCH
  if (pathname === "/customer/login") {
    const target = role === "Admin" ? "/admin" : "/customer/tickets";
    return NextResponse.redirect(new URL(target, request.url));
  }

  return NextResponse.next();
}

export const config = {
  // DODANO "/" DO MATCHERA
  matcher: ["/", "/admin/:path*", "/customer/:path*"],
};
