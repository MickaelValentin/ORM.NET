<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>

<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html>
<html lang="fr">
<head>
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<link rel="stylesheet"
	href="<c:url value="/bootstrap/css/bootstrap.min.css" />" />
<title>Insert title here</title>
	<script type="text/javascript" src="<c:url value='/jquery/jquery-3.1.1.min.js' />"></script>
	<script type="text/javascript" src="<c:url value='/bootstrap/js/bootstrap.min.js' />"></script>
</head>
<body>
	<nav class="navbar navbar-default">
		<div class="container-fluid">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle collapsed"
					data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"
					aria-expanded="false">
					<span class="sr-only">Toggle navigation</span> <span
						class="icon-bar"></span> <span class="icon-bar"></span> <span
						class="icon-bar"></span>
				</button>
				<a class="navbar-brand" href="<c:url value='/test/bonjour' />">Mastere</a>
			</div>

			<div class="collapse navbar-collapse">
				<ul class="nav navbar-nav navbar-right">
					
				<c:if test="${!empty sessionScope.UTILISATEUR }">
					<li><a href="<c:url value='/backoffice/liste_utilisateur' />">Utilisateurs</a></li>
					<li><a>Bonjour ${sessionScope.UTILISATEUR.prenom }</a></li>
					<li><a class="btn btn-primary" href="<c:url value='/logout'/>">Se déconnecter</a></li>
				</c:if>
					<li><a href="<c:url value='/backoffice'/>">Backoffice</a></li>
				</ul>
			</div>
		</div>
	</nav>
	
	<c:if test="${!empty errors }">
		<div class="alert alert-danger" role="alert">
			<ul>
				<c:forEach items="${errors}" var="e">
					<li>${e}</li>
				</c:forEach>
			</ul>
		</div>
	</c:if>
	
	<c:if test="${!empty infos }">
		<div class="alert alert-info" role="alert">
			<ul>
				<c:forEach items="${infos}" var="e">
					<li>${e}</li>
				</c:forEach>
			</ul>
		</div>
	</c:if>
	
	