package com.mastere.controler;

import java.io.IOException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.mastere.dao.UtilDAO;
import com.mastere.dao.UtilisateurDAO;
import com.mastere.metier.Groupe;
import com.mastere.metier.Utilisateur;

/**
 * Servlet implementation class ServletUtilisateur
 */
@WebServlet("/utilisateur/*")
public class ServletUtilisateur extends UtilHttpServlet {
	private static final long serialVersionUID = 1L;

    /**
     * Default constructor. 
     */
    public ServletUtilisateur() {
        
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		final String action = request.getPathInfo();
		final UtilisateurDAO ud = new UtilisateurDAO();
		switch (action) {
		case "/create":
			request.setAttribute("groupes", ud.getAllGroupe());
			afficherVue("create", request, response);
			break;

		default:
			response.sendError(404);
			break;
		}
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		final String action = request.getPathInfo();
		final UtilDAO<Groupe> gd = new UtilDAO<>(Groupe.class);
		final UtilisateurDAO ud = new UtilisateurDAO();
		
		switch (action) {
		case "/create":
			Utilisateur u = new Utilisateur();
			u.setMail(getParam("mail", request));
			u.setNom(getParam("nom", request));
			u.setPrenom(getParam("prenom", request));
			try {
				MessageDigest md = MessageDigest.getInstance("MD5");
				md.update(getParam("password", request).getBytes());
				byte byteData[] = md.digest();
				
				StringBuffer sb = new StringBuffer();
				for(int i=0; i <byteData.length; i++)
					sb.append(Integer.toString((byteData[i] & 0xff) + 0x100, 16).substring(1));
				u.setPassword(sb.toString());
			} catch (NoSuchAlgorithmException e) {
				e.printStackTrace();
			}
			
			
			
			Groupe groupe = gd.getById(getParamAsInt("groupe", request));
			u.setGroupe(groupe);
			ud.save(u);
			break;

		default:
			response.sendError(404);
			break;
		}
	}

}
