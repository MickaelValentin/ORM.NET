package com.mastere.controler;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.gson.Gson;
import com.mastere.dao.UtilisateurDAO;
import com.mastere.metier.Utilisateur;

import javassist.bytecode.stackmap.BasicBlock.Catch;

public class ServletBackoffice extends UtilHttpServlet {

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		final String action = req.getPathInfo() == null ? "/" : req.getPathInfo();
		switch (action) {
		case "/":
			afficherVue("dashboard", req, resp);
			break;
			
		case "/liste_utilisateur":
			UtilisateurDAO ud = new UtilisateurDAO();
			req.setAttribute("utilisateurs", ud.listAll());
			afficherVue("liste_utilisateur", req, resp);
			break;

		default:
			resp.sendError(404);
			break;
		}
	}

	@Override
	protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		final String action = req.getPathInfo() == null ? "/" : req.getPathInfo();
		switch (action) {
		case "/delete_utilisateur":
			try{
			long id = getParamAsInt("id", req);
			UtilisateurDAO ud = new UtilisateurDAO();
			Utilisateur u = ud.getById(id);
			//delete u
			
			Gson json = new Gson();
			json.toJson(u);
			
			resp.setContentType("application/json");
			resp.getWriter().write(json.toString());
			}catch(Exception e){
				e.printStackTrace();
			}
			break;
			
		default:
			resp.sendError(404);
			break;
		}
			
	}

}
