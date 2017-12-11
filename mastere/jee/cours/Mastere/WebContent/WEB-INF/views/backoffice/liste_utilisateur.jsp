<table class="table table-striped table-bordered">
	<tr>
		<th>Prénom</th>
		<th>Nom</th>
		<th>Mail</th>
		<th>Action</th>
	</tr>
	<c:forEach items="${utilisateurs }" var="u">
		<tr>
			<td>${u.prenom }</td>
			<td>${u.nom }</td>
			<td>${u.mail }</td>
			<td>
				<a href="#" class="btn btn-default suppr" data-id="${u.id}" >Supprimer</a>
			</td>
		</tr>
	</c:forEach>
</table>

<script type="text/javascript">
	$(document).ready(function(){
		$(".suppr").each(function(){
			$(this).click(function(){
				var idUtilisateur = $(this).data("id");
				var lien = $(this);
				//alert(idUtilisateur);
				$.ajax({
					url: "<c:url value='/backoffice/delete_utilisateur'/>",
					type: "POST",
					dataType: "json",
					mineType: "application/json",
					data: {id: idUtilisateur},
					success: function(obj){
						//alert("utilisateur supprimé: " + obj.nom);
						lien.parent().parent().fadeOut("slow");
					}
				});
				return false;
			});
		});
	});

</script>
