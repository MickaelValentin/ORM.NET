package com.mastere.controler;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletResponse;

import org.apache.catalina.servlet4preview.http.HttpServletRequest;

import com.sun.javafx.geom.transform.GeneralTransform3D;

public abstract class UtilHttpServlet extends HttpServlet {

	private static final String[]  VRAI = {"true", "on", "yes", "1", "checked"};
	
	protected List<String> errors = new ArrayList<>();
	protected List<String> infos = new ArrayList<>();
	
	protected void afficherVue(final String vue, final ServletRequest req, ServletResponse resp ) throws ServletException, IOException{
		String controleur = this.getClass().getSimpleName();
		controleur = controleur.substring(controleur.lastIndexOf("Servlet") + 7).toLowerCase();
		try{
			req.setAttribute("errors", errors);
			req.setAttribute("infos", infos);
			
			req.getRequestDispatcher("/WEB-INF/views/" + controleur + "/" + vue + ".jsp")
				.forward(req, resp);
		}catch(IOException e){
			((HttpServletResponse)resp).sendError(405, "Fichier vue introuvable");
		}
		errors.clear();
		infos.clear();
	}
	
	public static void redirectionExterne(final String url, final ServletResponse resp) throws IOException{
		((HttpServletResponse)resp).sendRedirect(url);
	}
	
	public static void redirectionInterne(String url, final ServletRequest req, final ServletResponse resp) throws IOException{
		url = ((HttpServletRequest)req).getContextPath() +url;
		((HttpServletResponse)resp).sendRedirect(url);
	}
	
	public static String getParam(final String nom, final ServletRequest req){
		final String resultat = req.getParameter(nom);
		return resultat != null ? resultat : "";
	}
	
	public static int getParamAsInt(final String nom, final ServletRequest req){
		final String resultat = getParam(nom, req);
		try{
			return Integer.parseInt(resultat);
		}catch(NumberFormatException e){
			return 0;
		}
	}
	
	public static float getParamAsFloat(final String nom, final ServletRequest req){
		final String resultat = getParam(nom, req);
		try{
			return Float.parseFloat(resultat);
		}catch(NumberFormatException e){
			return 0;
		}
	}
	
	public static boolean getParamAsBool(final String nom, final ServletRequest req){
		final String resultat = getParam(nom, req);
		for(final String s : VRAI)
			if(resultat.toLowerCase().equals(s))
				return true;
		return false;
	}
	
}
