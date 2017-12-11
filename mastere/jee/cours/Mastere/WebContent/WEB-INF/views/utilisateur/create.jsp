<form class="form-horizontal" method="post" action="#">
  <div class="form-group">
    <label for="inputEmail3" class="col-sm-2 control-label">Email</label>
    <div class="col-sm-10">
      <input name="mail" type="email" class="form-control" id="inputEmail3" placeholder="Email" />
    </div>
  </div>
  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Password</label>
    <div class="col-sm-10">
      <input name="password" type="password" class="form-control" id="inputPassword3" placeholder="Password" />
    </div>
  </div>
  <div class="form-group">
    <label for="inputNom" class="col-sm-2 control-label">Nom</label>
    <div class="col-sm-10">
      <input name="nom" type="text" class="form-control" id="inputNom" placeholder="Nom" />
    </div>
  </div>
  <div class="form-group">
    <label for="inputPrenom" class="col-sm-2 control-label">Prénom</label>
    <div class="col-sm-10">
      <input name="prenom" type="text" class="form-control" id="inputPrenom" placeholder="Nom" />
    </div>
  </div>
  <div class="form-group">
    <label  class="col-sm-2 control-label">Groupe</label>
    <div class="col-sm-10">
      <select name="groupe" class="form-control">
      <c:forEach var="groupe" items="${groupes}">
      	<option value="${groupe.id}">${groupe.libelle}</option>
      </c:forEach>
      </select>
    </div>
  </div>
  
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <button type="submit" class="btn btn-default">Enregistrer</button>
    </div>
  </div>
</form>