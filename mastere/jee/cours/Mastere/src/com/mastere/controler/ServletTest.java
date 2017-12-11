package com.mastere.controler;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.hibernate.Session;

public class ServletTest extends HttpServlet {

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		final String action = req.getPathInfo();
		
		//System.out.println("action: " + action);
		if(action == null){
			resp.sendError(404, "L'action demandée n'est pas prise en charge.");
			return;
		}
		switch(action){
			case "/google": 
				resp.sendRedirect("http://www.google.fr");
				break;
			case "/bonjour":
				req.getRequestDispatcher("/WEB-INF/views/test/bonjour.jsp").forward(req, resp);
				break;
			case "/formulaire":
				req.getRequestDispatcher("/WEB-INF/views/test/formulaire.jsp").forward(req, resp);
				break;
			default:
				resp.sendError(404, "L'action demandée n'est pas prise en charge.");
				break;
		}
	}

	@Override
	protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		final String action = req.getPathInfo();
		
		if(action.equals("/formulaire")){
			int a = Integer.parseInt(req.getParameter("inputA"));
			int b = Integer.parseInt(req.getParameter("inputB"));
			
			int resultat = a + b;
			
			req.setAttribute("resultat", resultat);
			req.setAttribute("a", a);
			req.setAttribute("b", b);
			req.getRequestDispatcher("/WEB-INF/views/test/resultat.jsp").forward(req, resp);
			return;
		}
		
		resp.sendError(404, "L'action demandée n'est pas prise en charge.");
	}

}
