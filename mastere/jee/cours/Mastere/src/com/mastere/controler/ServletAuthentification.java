package com.mastere.controler;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.mastere.dao.UtilisateurDAO;
import com.mastere.metier.Utilisateur;

public class ServletAuthentification extends UtilHttpServlet {

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		final String path = req.getServletPath();
		if(path.equals("/login")){
			afficherVue("login", req, resp);
			return;
		}
		if(path.equals("/logout")){
			req.getSession().removeAttribute("UTILISATEUR");
			redirectionInterne("/test/bonjour", req, resp);
			return;
		}
		
	}

	@Override
	protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		final UtilisateurDAO ud = new UtilisateurDAO();
		
		String mail = getParam("mail", req);
		String password = getParam("password", req);
		
		if(mail.equals(""))
			this.errors.add("Le mail est obligatoire");
		if(password.equals(""))
			this.errors.add("Le mot de passe est obligatoire");
		
		Utilisateur u = ud.getByMailPassword(mail, password);
		if(u == null)
			this.errors.add("Login ou mot de passe incorrect.");
		
		if(errors.size() != 0)
			afficherVue("login", req, resp);
		else{
			req.getSession().setAttribute("UTILISATEUR", u);
			redirectionInterne("/backoffice", req, resp);
		}
		
	}

}
