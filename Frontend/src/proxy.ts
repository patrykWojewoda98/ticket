import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export function proxy(request: NextRequest) {
  const role = request.cookies.get("user_role")?.value;
  const { pathname } = request.nextUrl;

 
  if (pathname === "/" && role === "Admin") {
    return NextResponse.redirect(new URL("/admin", request.url));
  }

  
  if (!role) {
    if (pathname.startsWith("/admin") || (pathname.startsWith("/customer") && pathname !== "/customer/login")) {
      return NextResponse.redirect(new URL("/customer/login", request.url));
    }
    return NextResponse.next();
  }

  
  if (pathname.startsWith("/admin")) {
    if (role !== "Admin") {
      return NextResponse.redirect(new URL("/customer/tickets", request.url));
    }
  }

 
  if (pathname.startsWith("/customer") && pathname !== "/customer/login") {
    if (role === "Admin") {
      return NextResponse.redirect(new URL("/admin", request.url));
    }
  }

 
  if (pathname === "/customer/login") {
    const target = role === "Admin" ? "/admin" : "/customer/tickets";
    return NextResponse.redirect(new URL(target, request.url));
  }

  return NextResponse.next();
}

export const config = {
  
  matcher: ["/", "/admin/:path*", "/customer/:path*"],
};
